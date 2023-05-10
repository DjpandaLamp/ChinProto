using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class NewAIControler : MonoBehaviour
{
    public enum aIMode { Chase, Node }
    public enum lookForLayer { Low, Mid, High }
    [SerializeField]
    public LayerMask _layerMask;

    [Header("AI Setting")]
    public aIMode AIMode;
    public bool IsAvoidingCars = true;


    //Local Variables
    [Header("DebugCheck")]
    public Vector3 targetPosition = Vector3.zero;
    public Transform targetTransform = null;


    //Avoidance
    Vector2 avoidanceVectorLerped = Vector2.zero;

    //Waypoints
    WaypointNode currentWaypoint = null;
    WaypointNode previousWaypoint = null;
    WaypointNode[] allWaypoints;
    //Components 
    private PlayerMovement PlayerMovement;
    PolygonCollider2D poly;

    void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        allWaypoints = FindObjectsOfType<WaypointNode>();
        poly = GetComponent<PolygonCollider2D>();
        
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
            previousWaypoint = currentWaypoint;
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
                previousWaypoint = currentWaypoint;
                currentWaypoint = currentWaypoint.nextNode[Random.Range(0, currentWaypoint.nextNode.Length)];
            }
        }

    }
    
    bool CheckInFrontCar(out Vector3 position, out Vector3 otherCarRightVector)
    {

        
        poly.enabled = false;
        //Check for other cars using a circle raycast
        
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + transform.up * 3f, 1f, transform.up, 6, _layerMask);
        

        poly.enabled = true; 

        
        if (raycastHit2D.collider != null)
        {
            Debug.DrawRay(transform.position + transform.up, transform.up * 6, Color.red);
            

            position = raycastHit2D.collider.transform.position;

            otherCarRightVector = raycastHit2D.collider.transform.right;

            return true;
        }
        else
        {
            //Black Line for not hitting
            Debug.DrawRay(transform.position + transform.up, transform.up * 6, Color.blue);
            
        }
        
        //Nothing Hit Raycast
        position = Vector3.zero;
        otherCarRightVector = Vector3.zero;
         
        return false;
    }
    //finds the closest point of a line on the path of the next checkpoint
    Vector2 FindNearestPointOnLine(Vector2 lineStartPos, Vector2 lineEndPos, Vector2 point)
    {
        //get the heading vector
        Vector2 lineHeadingVector = lineEndPos - lineStartPos;

        //Store Max distance
        float maxDistane = lineHeadingVector.magnitude;
        lineHeadingVector.Normalize();
    }


    void AvoidCars(Vector2 vectorToTarget, out Vector3 newVectorToTarget)
    {
        if(CheckInFrontCar(out Vector3 position, out Vector3 otherCarRightVector))
        {
            Vector2 avoidVector = Vector2.zero;

            avoidVector = Vector2.Reflect((position - transform.position).normalized, otherCarRightVector);

            //Distance to the Target
            float distanceToTarget = (targetPosition - transform.position).magnitude;

            //Levels the desire between wanting to avoid cars and wants to finish the face
            //The closer to the waypoint the weaker the avoidance becomes

            float driveToTargetInfluence = 6.0f / distanceToTarget;
            //Limits the Value to avoidance between 30% and 100%
            driveToTargetInfluence = Mathf.Clamp(driveToTargetInfluence, 0.30f, 1f);

            //Desire to drive away is inverse the distance to the waypoint
            float avoidanceInflence = 1.0f - driveToTargetInfluence;

            //Reduce Jittery Movement
            avoidanceVectorLerped = Vector2.Lerp(avoidanceVectorLerped, avoidVector, Time.fixedDeltaTime * 4);

            newVectorToTarget = (vectorToTarget*driveToTargetInfluence) + (avoidanceVectorLerped * avoidanceInflence);
            newVectorToTarget.Normalize();


            //Shows The vector that the avoidance is prioviding
            Debug.DrawRay(transform.position, avoidVector * 10, Color.green);
            //Shows actual vector of movement
            Debug.DrawRay(transform.position, newVectorToTarget * 10, Color.yellow);
            return;
        }
        newVectorToTarget = vectorToTarget;
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

        if (IsAvoidingCars)
        {
            AvoidCars(vectorToTarget, out vectorToTarget);
        }
        

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



