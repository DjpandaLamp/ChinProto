using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float accelerationSet = 2f; //Acceleration Multiplier
    public float turnSet = 3.5f;
    public float driftSet = 0.95f;
    public float maxSpeed = 3f;
    public float MultSpeed = 1.0f;
    public float accelerationInput;
    public float steerInput;

    public float rotateAngle = 0;

    public float velocityVsUp;

    Rigidbody2D rb;
    LevelDefine LevelDefine;

    void Awake()
    {
        LevelDefine = GameObject.FindGameObjectWithTag("LevelDefine").GetComponent<LevelDefine>();
        rb = GetComponent<Rigidbody2D>();
        
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

        if (velocityVsUp > maxSpeed * MultSpeed && accelerationInput > 0)
        {
            return;
        }
        if (velocityVsUp < -maxSpeed * 0.5f * MultSpeed && accelerationInput < 0)
        {
            return;
        }
        if (rb.velocity.sqrMagnitude > (maxSpeed * maxSpeed) * MultSpeed && accelerationInput > 0)
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

        Vector3 accelVector = transform.up * accelerationInput * accelerationSet * MultSpeed;

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
