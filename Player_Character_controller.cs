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

public class Player_Character_controller : MonoBehaviour
{
    enum States //defines states
    {
        idle,
        walking,
        dash,
        useItem
    }

    enum Items //tbc
    {
        NitroFuel,
        RoadSpike,
        FlameBall
    }


    public Rigidbody2D rb;
    public float xDirection;
    public float yDirection;
    public Vector3 velocity;
    public float maxSpeed;
    public float acceleration = 3;
    public float steerMag = 3;

    [SerializeField]
    States state; //allows interaction with the enum States
    [SerializeField]
    States previousFrameState; //Helps with Transitions between states
    public LevelDefine LevelDefineCharacteristics; //Allows for interaction with the level data
    
    private AI_Controler target;
    private CameraFollow cameraFollow;
    private MasterBarScript masterBar;
    public AIPath AIPath;
    public AIDestinationSetter AIFinder;
    public Animator p_animator; //allows easy interaction with animator
    public SpriteRenderer p_spriteRenderer;
    PlayerMovement PlayerMovement;

    public Text StateText; //Text representation current state
    public Text moneyText; //Text representation of Amount of money 
    public Text lapText; //Text representation of the current lap count
    
    
    public int Money; //Amount of Money
    public float DashTime; //Amount of time left in a dash
    public float dashCooldown = 1; //Cooldown of a dash 
    public string heldItem; //Currently Held Item
    public int currentCheckpointSelf; //Current Checkpoint Number for the player
    public int lapNumber = 1; //Current lap
    public int maxLapNumber = 4; //Maximum Number of laps
    public int displayLap;


    public float playerSpeed = 1;
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
    public int test;
    public int currentLayer;

    void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        state = States.idle;
        StartCoroutine(LateStart(0.2f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        state = States.idle;
//        masterBar = GameObject.Find("HelathBar").GetComponent<MasterBarScript>();
        
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
        previousFrameState = state;
        if (canControl == true)
        {
            Vector2 inputVector = Vector2.zero;
            //Get Horizontal Input
            inputVector.x = (Input.GetAxis("Horizontal"));
            
            //Get Vertical Input
            inputVector.y = (Input.GetAxis("Vertical"));

            PlayerMovement.SetInputVector(inputVector);




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

            PlayerMovement.MultSpeed = 1; ;
            DashMult = Mathf.Lerp(DashMult, 0, 0.18f * Time.deltaTime);
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
            PlayerMovement.MultSpeed = 1;



        }
    }

    void DashState()
    {
        DashShow = DashMult;
        if (DashMult <= 0.8)
        {
            DashMult = 2.5f;
        }
        
        
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