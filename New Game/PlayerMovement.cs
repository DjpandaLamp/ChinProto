using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float accelerationSet = 2f;
    public float turnSet = 3.5f;
    public float driftSet = 0.95f;
    public float maxSpeed = 3f;
    public float MultSpeed = 1.0f;
    float accelerationInput;
    float steerInput;

    public float rotateAngle = 0;

    float velocityVsUp;

    Rigidbody2D rb;
    LevelDefine LevelDefine;

    void Awake()
    {
        LevelDefine = GameObject.FindGameObjectWithTag("LevelDefine").GetComponent<LevelDefine>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ApplyAcceleration();

        SetSideVelocity();

        ApplySteer();
    }

    void ApplyAcceleration()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }


        if (accelerationInput==0)
        {
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            rb.drag = 0;
        }

        Vector3 accelVector = transform.up * accelerationInput * accelerationSet;

        rb.AddForce(accelVector, ForceMode2D.Force);
    }

    void ApplySteer()
    {
      
            float minSpeed = (rb.velocity.magnitude / 4);
            minSpeed = Mathf.Clamp01(minSpeed);
            rotateAngle -= steerInput * turnSet * minSpeed;
           
            rb.MoveRotation(rotateAngle);

    }

    void SetSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 sideVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + sideVelocity * driftSet;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steerInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
