using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class NewAIControler : MonoBehaviour
{
    public enum aIMode { Chase, Node }

    [SerializeField]
    private LayerMask _layerMask;

    [Header("AI Setting")]
    public aIMode AIMode;
    public bool IsAvoidingCars = true;
    //Local Variables
    [Header("DebugCheck")]
    public Vector3 targetPosition = Vector3.zero;
    public Transform targetTransform = null;

    //Waypoints
    WaypointNode currentWaypoint = null;
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
    
    bool CheckInFrontCar(out Vector3 position, out Vector3 otherCarRightVector)
    {


        poly.enabled = false;
        //Check for other cars using a circle raycast

        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + transform.up * 3f, 2, transform.up, 6, _layerMask);
        

        poly.enabled = true; 

        
        if (raycastHit2D.collider != null)
        {
            Debug.DrawRay(transform.position + transform.up + transform.right, transform.up * 6, Color.red);
            Debug.DrawRay(transform.position + transform.up - transform.right, transform.up * 6, Color.red);

            position = raycastHit2D.collider.transform.position;

            otherCarRightVector = raycastHit2D.collider.transform.right;

            return true;
        }
        else
        {
            //Black Line for not hitting
            Debug.DrawRay(transform.position + transform.up + transform.right, transform.up * 6, Color.blue);
            Debug.DrawRay(transform.position + transform.up - transform.right, transform.up * 6, Color.blue);
        }
        
        //Nothing Hit Raycast
        position = Vector3.zero;
        otherCarRightVector = Vector3.zero;
         
        return false;
    }

    void AvoidCars()
    {
        CheckInFrontCar(out Vector3 positionn, out Vector3 otherCarRightVector);

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
            AvoidCars();
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



