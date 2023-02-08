using Pathfinding.Ionic.Zip;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AI_Controler : Character_Controller
{
    private Rigidbody2D body;
    public int currentCheckpoint; //Current Checkpoint Number
    public int lapNumber = 1; //Current lap
    private Checkpoint_Controler checkpoint_Controler;
    private LevelDefineCharacteristics characteristics;
    public int[] AIPlayer;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        characteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefineCharacteristics>();
        checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
        transform.position = new Vector3(checkpoint_Controler.transform.position.x, checkpoint_Controler.transform.position.y, checkpoint_Controler.transform.position.z);
        body.rotation = checkpoint_Controler.transform.eulerAngles.z;
        
        transform.localScale = new Vector3(checkpoint_Controler.transform.localScale.x, checkpoint_Controler.transform.localScale.y, checkpoint_Controler.transform.localScale.z);
        
        AIPlayer = new int[characteristics.levelContestants];
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkpoint")
        {

            currentCheckpoint = checkpoint_Controler.CheckpointNumber;

        }
        for (int i = 0; i < AIPlayer.Length; i++)
        {
            if (collision.gameObject.tag == "AIPlayer" + i && gameObject.name == "Target_0" + i) //AI Player 1 Collision
            {
                if (GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)) != null) //Checks if the next checkpoint exists. if not, reset the checkpoint counter and increment players laps 
                {
                    checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
                }
                else
                {
                    currentCheckpoint = 0;
                    characteristics.levelPlayerLaps[i] += 1;
                    checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
                }

                transform.position = new Vector3(checkpoint_Controler.transform.position.x, checkpoint_Controler.transform.position.y, checkpoint_Controler.transform.position.z);
                body.rotation = checkpoint_Controler.transform.eulerAngles.z;
                transform.localScale = new Vector3(checkpoint_Controler.transform.localScale.x, checkpoint_Controler.transform.localScale.y, checkpoint_Controler.transform.localScale.z);


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
                    characteristics.levelPlayerLaps[4] += 1;
                    checkpoint_Controler = GameObject.Find("Checkpoint_" + (currentCheckpoint + 1)).GetComponent<Checkpoint_Controler>();
                }

                transform.position = new Vector3(checkpoint_Controler.transform.position.x, checkpoint_Controler.transform.position.y, checkpoint_Controler.transform.position.z);
                body.rotation = checkpoint_Controler.transform.eulerAngles.z;
                transform.localScale = new Vector3(checkpoint_Controler.transform.localScale.x, checkpoint_Controler.transform.localScale.y, checkpoint_Controler.transform.localScale.z);
            }

        }
    }
}