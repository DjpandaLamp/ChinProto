using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.VersionControl.Asset;

public class WaypointNode : MonoBehaviour
{
    [Header("Settings")]
    public WaypointNode[] nextNode;
    public float minDistanceToNextNode;
    public bool isStartingCheckpoint = false;
    public int checkpointValue;
    public int maxCheckpointValue;

    private BoxCollider2D BoxCollider2D;
    private Player_Character_controller player_Character_Controller;
    private PolygonCollider2D playerPolygon;
 
    

    private void Start()
    {
        
  
    StartCoroutine(LateStart(0.2f));
    }

IEnumerator LateStart(float waitTime)
{
    yield return new WaitForSeconds(waitTime);
    BoxCollider2D = this.GetComponent<BoxCollider2D>();
    player_Character_Controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Character_controller>();
        playerPolygon = GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>();




}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == playerPolygon)
        {

            if (player_Character_Controller.currentCheckpointSelf >= maxCheckpointValue && isStartingCheckpoint)
            {
                player_Character_Controller.currentCheckpointSelf = 0;
                player_Character_Controller.lapNumber += 1;
            }
            else if (player_Character_Controller.currentCheckpointSelf != maxCheckpointValue && checkpointValue == player_Character_Controller.currentCheckpointSelf+1)
            {
                player_Character_Controller.currentCheckpointSelf = checkpointValue;
            }
            if (player_Character_Controller.maxLapNumber == player_Character_Controller.lapNumber)
            {
                SceneManager.LoadSceneAsync("rMenu");
            }
        }
    }


    void OnDrawGizmos()
    {
        for (int i=0; i <nextNode.Length; i++)
        {
            Gizmos.DrawLine(transform.position, nextNode[i].transform.position);
            Gizmos.DrawWireSphere(transform.position, minDistanceToNextNode);
        }
       
    }
}
