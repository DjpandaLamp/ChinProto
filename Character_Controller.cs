using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    enum Items
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
