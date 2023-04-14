using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class NewAIControler : MonoBehaviour
{
    public enum aIMode { Chase, Node }

    [Header("AI Setting")]
    public aIMode AIMode;

    //Local Variables
    [Header("DebugCheck")]
    public Vector3 targetPosition = Vector3.zero;
    public Transform targetTransform = null;

    //Waypoints
    WaypointNode currentWaypoint = null;
    WaypointNode[] allWaypoints;
    //Components 
    private PlayerMovement PlayerMovement;

    void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        allWaypoints = FindObjectsOfType<WaypointNode>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
        switch (AIMode)
        {
            case aIMode.Chase:
                Chase();
                break;

            case aIMode.Node:
                NodeFollow();
                break;

        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = SpeedControl(inputVector.x);
        PlayerMovement.SetInputVector(inputVector);
    }


    void Chase()
    {
        if (targetTransform == null)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }


    }
    void NodeFollow()
    {
        if (currentWaypoint == null)
        {
            currentWaypoint = FindNearestWaypoint();
        }
        if (currentWaypoint !=null)
        {
            targetPosition = currentWaypoint.transform.position;
            //Check Distance to target
            Vector2 baseVector = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
            float DistanceToWaypoint = baseVector.magnitude;
            //Check if close enough to reach waypoint
            if (DistanceToWaypoint <= currentWaypoint.minDistanceToNextNode)
            {
                currentWaypoint = currentWaypoint.nextNode[Random.Range(0, currentWaypoint.nextNode.Length)];
            }
        }

    }

    WaypointNode FindNearestWaypoint()
    {
        return allWaypoints
        .OrderBy(t => Vector2.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }
    float TurnTowardTarget()
    {
        //get vector
        Vector3 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();
        //calculate angle
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;
        //Clamp angle and smooth
        float steerAmount = angleToTarget / 45f;
        steerAmount = Mathf.Clamp(steerAmount, -1f, 1f);

        return steerAmount;
    }
    float SpeedControl(float SteerInput)
    {
        return 1.05f - Mathf.Abs(SteerInput) / 0.75f;
    }
}



