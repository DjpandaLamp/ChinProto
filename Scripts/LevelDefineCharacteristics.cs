using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelDefineCharacteristics : MonoBehaviour
{
    private Player_Character_controller player_Character_Controller;
    private CameraFollow m_camera;
    private PauseShade PauseShade;
    public string levelName;
    public float levelSpeed = 1f;
    public bool gamePaused = false;
    public int levelLapsCount;
    public int levelContestants;
    public int levelCheckpointRollover;

    public bool isLevelComplete;
    public float[] levelPlayerTime;
    public int[] levelPlayerLaps;
    public bool[] levelPlayerComplete;
    public float[] distanceToNextCheckpoint;
    public float[] currentPosition;
    //public string[] sortedPosition;
    public int[] indices; 
    private Path[] ap;
    private AIPath[] AIPath;
    private AI_Controler[] AI_Controlers;
    private Checkpoint_Controler[] Checkpoint_Controlers;

    public GameObject AIPlayer;






    // Start is called before the first frame update
    void Start()
    {
        levelPlayerTime = new float[levelContestants];
        levelPlayerComplete = new bool[levelContestants];
        levelPlayerLaps = new int[levelContestants];
        AIPath = new Pathfinding.AIPath[levelContestants];
        distanceToNextCheckpoint = new float[levelContestants];
        currentPosition = new float[levelContestants];
        //sortedPosition = new string[levelContestants];
        indices = new int[levelContestants];
        
        AI_Controlers = new AI_Controler[levelContestants];
        Checkpoint_Controlers = new Checkpoint_Controler[levelContestants];
        

        player_Character_Controller = GameObject.Find("Player").GetComponent<Player_Character_controller>();
        m_camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        PauseShade = GameObject.Find("PauseShade").GetComponent<PauseShade>();

        for (int i = 0; i < levelContestants; i++)
        {
            if (GameObject.Find("AI Player_0" + i) == null)
            {
                levelPlayerLaps[i] = player_Character_Controller.lapNumber;
                break;
            }
            AIPath[i] = GameObject.Find("AI Player_0" + i).GetComponent<AIPath>();
            AI_Controlers[i] = GameObject.Find("Target_0" + i).GetComponent<AI_Controler>();
            Checkpoint_Controlers[i] = GameObject.Find("Checkpoint_" + (AI_Controlers[i].currentCheckpoint+1)).GetComponent<Checkpoint_Controler>();
            levelPlayerLaps[i] = 1;
            
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelTimeSetter();
        userPosition();



        //UI and Level Setter
        if (player_Character_Controller != null)
        {
            SetLapPlayer();
            SetText();
        }

        
        PauseGame();
    }

    void SetLapPlayer()
    {
        if (player_Character_Controller.currentCheckpoint > levelCheckpointRollover)
        {
            player_Character_Controller.currentCheckpoint = 0;
            player_Character_Controller.lapNumber += 1;
            levelPlayerLaps[levelContestants-1] = player_Character_Controller.lapNumber;
        }

    }
    
    void LevelTimeSetter()
    {
        if (gamePaused == false) 
        {

            for (int i = 0; i < levelContestants; i++) 
            {
                if (levelPlayerLaps[i] >= levelLapsCount) //Checks if the player has completed the level by checking if the player has completed the required laps, as determined by levelLapsCount

                {
                    levelPlayerComplete[i] = true; 
                }

                if (!levelPlayerComplete[i]) //if Ai Player or Player haven't completed the level, incremnt their time by time passed
                {
                    levelPlayerTime[i] += Time.deltaTime;
                }
                else if (levelPlayerComplete[i] && AIPath[i] == null)
                {
                    var NewAIPlayer = Instantiate(AIPlayer, player_Character_Controller.transform.position, player_Character_Controller.transform.rotation); //Spawns AI Player at the players position with the players rotation
                    player_Character_Controller.gameObject.SetActive(false); //Disables the player
                    NewAIPlayer.name = "AI Player_0" + i;
                    NewAIPlayer.tag = "AIPlayer" + i;
                    AIPath[i] = NewAIPlayer.GetComponent<AIPath>();
                    var _Dest = NewAIPlayer.GetComponent<AIDestinationSetter>();
                    _Dest.target = GameObject.Find("Target_0" + i).transform;
                    m_camera.Target = NewAIPlayer.transform;
                }
            }
        }
    }

    void userPosition()
    {
        for (int i = 0; i < levelContestants-1; i++) //For each player in a level, do 1 loop
        {


            int checkpoint = AI_Controlers[i].currentCheckpoint-1;
            Vector3 nextCheckpointPosition = new Vector3(AI_Controlers[i].transform.position.x, AI_Controlers[i].transform.position.y, AI_Controlers[i].transform.position.z);
            Vector3 playerPosition = new Vector3(AIPath[i].transform.position.x, AIPath[i].transform.position.y, AIPath[i].transform.position.z);
            if (AIPath[i] == null)
            {
                checkpoint = player_Character_Controller.currentCheckpoint-1;
                playerPosition = new Vector3(player_Character_Controller.transform.position.x, player_Character_Controller.transform.position.y, player_Character_Controller.transform.position.z);
                nextCheckpointPosition = new Vector3(Checkpoint_Controlers[player_Character_Controller.currentCheckpoint + 1].transform.position.x, Checkpoint_Controlers[player_Character_Controller.currentCheckpoint + 1].transform.position.y, Checkpoint_Controlers[player_Character_Controller.currentCheckpoint + 1].transform.position.z);
            }
            float AbsDistance = Vector3.Distance(nextCheckpointPosition, playerPosition);
            AbsDistance = Mathf.Abs(AbsDistance);
            AbsDistance = AbsDistance-(1000*checkpoint)-(100000 * (levelPlayerLaps[i]-1));
            currentPosition[i] = AbsDistance;
            
        }
        for (int i = 0; i < currentPosition.Length; i++)
        {
            indices[i] = i;
         
            /*
            currentPosition[i] = Mathf.Round(currentPosition[i]);
            sortedPosition[i] = currentPosition[i].ToString();
            if (i == 0)
            {
                sortedPosition[i] = sortedPosition[i] + "AI1";
            }
            if (i == 1)
            {
                sortedPosition[i] = sortedPosition[i] + "AI2";
            }
            if (i == 2)
            {
                sortedPosition[i] = sortedPosition[i] + "AI3";
            }
            if (i == 3)
            {
                sortedPosition[i] = sortedPosition[i] + "AI4";
            }
            if (i == 4)
            {
                sortedPosition[i] = sortedPosition[i] + "PLR";
            }
            */
        }

        Array.Sort(currentPosition, indices);
    }

 
 
   

    void SetText()
    {
        player_Character_Controller.moneyText.text = "x" + player_Character_Controller.Money.ToString();
        player_Character_Controller.lapText.text = "Laps: " + player_Character_Controller.lapNumber.ToString() + "/" + levelLapsCount.ToString();

    }


    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused == true)
            {
                gamePaused = false;
            }
            else
            {
                gamePaused = true;
            }

        }

        if (gamePaused == true)
        {
            levelSpeed = 0;
            
            PauseShade.opacity = Mathf.Lerp(PauseShade.opacity, 0.5f, 0.01f);
            for (int i = 0; i < levelContestants; i++)
            {
                if (AIPath[i] == null)
                {
                    continue;
                }
                AIPath[i].canMove = false;
            }


        }
        else
        {
            levelSpeed = 1;
            
            PauseShade.opacity = Mathf.Lerp(PauseShade.opacity, 0, 0.01f);
            for (int i = 0; i < levelContestants; i++)
            {
                if (AIPath[i] == null)
                {
                    continue;
                }
                AIPath[i].canMove = true;
            }

        }

        if (player_Character_Controller != null)
        {
            player_Character_Controller.playerSpeed = levelSpeed;
        }
        

    }
    
}