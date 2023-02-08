using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using Unity.Mathematics;
using System;
using System.Runtime.CompilerServices;
using Pathfinding;
using UnityEditor;
using System.Linq;

public class Player_Character_controller : Character_Controller
{
    enum States //defines states
    {
        idle,
        walking,
        dash,
        useItem
    }
    [SerializeField]
    States state; //allows interaction with the enum States
    [SerializeField]
    States previousFrameState; //Helps with Transitions between states
    public LevelDefineCharacteristics LevelDefineCharacteristics; //Allows for interaction with the level data
    private Checkpoint_Controler checkpoint_Controler; //Allows for interaction with checkpoint data
    private AI_Controler target;
    private CameraFollow cameraFollow;
    private MasterBarScript masterBar;
    public AIPath AIPath;
    public AIDestinationSetter AIFinder;
    public Animator p_animator; //allows easy interaction with animator
    public SpriteRenderer p_spriteRenderer;
   

    public Text StateText; //Text representation current state
    public Text moneyText; //Text representation of Amount of money 
    public Text lapText; //Text representation of the current lap count
    
    
    public int Money; //Amount of Money
    public float DashTime; //Amount of time left in a dash
    public float dashCooldown = 1; //Cooldown of a dash 
    public string heldItem; //Currently Held Item
    public int currentCheckpoint; //Current Checkpoint Number
    public int lapNumber = 1; //Current lap

    public float playerSpeed;
    public float invTime;
    public bool isInvincible;
    public bool canControl = true;
    public float DashMult = 1;
    public float DashShow = 1;


    private float currentSpeed;

    //inputs
    public bool dash;
    public bool useItem;

    public int clearTimer;

    // Start is called before the first frame update
    void Start()
    {
        
        state = States.idle;
        LevelDefineCharacteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefineCharacteristics>();
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        masterBar = GameObject.Find("HelathBar").GetComponent<MasterBarScript>();
        target = GameObject.Find("Target_04").GetComponent<AI_Controler>();
        this.p_animator = GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody2D>();
    }

    //Detects Colisions with Collider2D
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "money")
        {
            collision.gameObject.SetActive(false);
            Money++;
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash = true;
        }
    }

    //Updates game at fixed intervels 
    private void FixedUpdate()
    {
        currentCheckpoint = target.currentCheckpoint;
        previousFrameState = state;
        if (canControl == true)
        {
            //Get Horizontal Input
            xDirection = (Input.GetAxisRaw("Horizontal"));
            //Get Vertical Input
            yDirection = (Input.GetAxis("Vertical"));
            //Clear Inputs



            if (state == States.idle)
            {
                IdleState();
            }
            else if (state == States.walking)
            {
                WalkingState();
            }
            else if (state == States.dash)
            {
                DashState();
            }
            else if (state == States.useItem)
            {
                ItemState();
            }

            if (clearTimer <= 0)
            {
                dash = false;


                clearTimer = 5;
            }

            MovementSet(1);
            DashMult = Mathf.Lerp(DashMult, 0, 0.18f*Time.deltaTime);
            clearTimer -= 1;

        }
    }

    void IdleState()
    {
        if (xDirection != 0f || yDirection != 0f)
        {
            state = States.walking;
        }
        else
        {
            
        }
    }

    void WalkingState()
    {
        if (xDirection == 0f && yDirection == 0f)
        {
            state = States.idle;
        }
        else if (dash && DashMult <= 0.8)
        {
            dash = false;
            state = States.dash;
            
            p_animator.SetTrigger("playerDash");
            p_animator.ResetTrigger("playerWalk");
        }
        else
        {
            MovementSet(1);
            //update position
            transform.position = transform.position + velocity;

        }
    }

    void DashState()
    {
        DashShow = DashMult;
        if (DashMult <= 0.8)
        {
            DashMult = 2.5f;
        }
        MovementSet(DashMult);
        transform.position = transform.position + velocity;
        if (DashMult <= 1.4f)
        {
            p_animator.SetTrigger("playerWalk");
            p_animator.ResetTrigger("playerDash");
        }
        
        if (DashMult <= 1)
        {
            state = States.walking;
            return;
        } 
        
    }

    void ItemState()
    {

    }

    void MovementSet(float moveMult)
    {
            //Credit: https://answers.unity.com/questions/686025/top-down-2d-car-physics-1.html -- kdorland
            //Calc speed from input
            velocity = transform.up * (yDirection * acceleration * moveMult * playerSpeed);
            rb.AddForce(velocity);
            masterBar.setSlider(yDirection*DashShow);

        //rotate player
        float direction = Vector3.Dot(rb.velocity, rb.GetRelativeVector(Vector3.up));
            if (direction >= 0.0f)
            {
                rb.rotation -= xDirection * steerMag * playerSpeed;
            }
            else
            {
                rb.rotation += xDirection * steerMag * playerSpeed;
            }


            //Change Velocity based on rotation
            float driftforce = Vector3.Dot(rb.velocity, rb.GetRelativeVector(Vector3.left)) * 2.0f;
            Vector3 relativeforce = Vector3.right * driftforce;
            Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeforce), Color.red);
            rb.AddForce(rb.GetRelativeVector(relativeforce));

            //Max Speed
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            currentSpeed = rb.velocity.magnitude;
            rb.velocity = rb.velocity * 0.95f * Time.deltaTime;
     
    }


    void PlayerHurt()
    {
        if (isInvincible == false)
        {
            isInvincible = true;
            invTime = 60;
            StartCoroutine(BlinkShader());

        }

    }
    

    public IEnumerator BlinkShader()
    {
        do
        {
            p_spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            p_spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        } while (isInvincible);
        
    }
     

}