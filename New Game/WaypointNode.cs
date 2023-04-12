using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    [Header("Waypoint we are going toward, not yet reached")]
public WaypointNode[] nextNode;
    public float minDistanceToNextNode;



    void OnDrawGizmos()
    {
        for (int i=0; i <nextNode.Length; i++)
        {
            Gizmos.DrawLine(transform.position, nextNode[i].transform.position);
        }
       
    }
}
