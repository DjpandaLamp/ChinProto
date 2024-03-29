using Pathfinding.Ionic.Zip;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AI_Controler : MonoBehaviour
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

    private Rigidbody2D body;
    public int currentCheckpoint; //Current Checkpoint Number
    public int lapNumber = 1; //Current lap
    private Checkpoint_Controler checkpoint_Controler;
    private LevelDefine define;
    public int[] AIPlayer;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        define = GameObject.Find("LevelDefine").GetComponent<LevelDefine>();
        checkpoint_Controler = GameObject.Find("Checkpoint_1").GetComponent<Checkpoint_Controler>();
        transform.position = new Vector3(checkpoint_Controler.transform.position.x, checkpoint_Controler.transform.position.y, checkpoint_Controler.transform.position.z);
        body.rotation = checkpoint_Controler.transform.eulerAngles.z;
        
        transform.localScale = new Vector3(checkpoint_Controler.transform.localScale.x, checkpoint_Controler.transform.localScale.y, checkpoint_Controler.transform.localScale.z);
        
        
        StartCoroutine(LateStart(0.25f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        AIPlayer = new int[define.count];
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        if (checkpoint_Controler.CheckpointNumber > currentCheckpoint)
        {

            currentCheckpoint = checkpoint_Controler.CheckpointNumber;
            Debug.Log("Triangles");
        }
        for (int i = 0; i < AIPlayer.Length; i++)
        {
            if (collision.gameObject.tag == "AIPlayer" + i && gameObject.name == "Target_0" + i) //AI Player Collision
            {
                if (GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)) != null) //Checks if the next checkpoint exists. if not, reset the checkpoint counter and increment players laps 
                {
                    checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
                    
                }
                else
                {
                   
                    currentCheckpoint = 0;
                    define.levelPlayerLaps[i] += 1;
                    checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
                }

                transform.position = new Vector3(checkpoint_Controler.transform.position.x, checkpoint_Controler.transform.position.y, checkpoint_Controler.transform.position.z);
                body.rotation = checkpoint_Controler.transform.eulerAngles.z;
                transform.localScale = new Vector3(checkpoint_Controler.transform.localScale.x, .74f, checkpoint_Controler.transform.localScale.z);


            }
            else if (collision.gameObject.tag == "Player" && gameObject.name == "Target_04")
                {
                if (GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)) != null) //Checks if the next checkpoint exists. if not, reset the checkpoint counter and increment players laps 
                {
                    checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
                }
                else
                {
                    currentCheckpoint = 0;
                    define.levelPlayerLaps[4] += 1;
                    checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
                }

                transform.position = new Vector3(checkpoint_Controler.transform.position.x, checkpoint_Controler.transform.position.y, checkpoint_Controler.transform.position.z);
                body.rotation = checkpoint_Controler.transform.eulerAngles.z;
                transform.localScale = new Vector3(checkpoint_Controler.transform.localScale.x, .74f, checkpoint_Controler.transform.localScale.z);
            }

        }
    }
}