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
    public int[] currentCheckpoint;
    //public string[] sortedPosition;
    public int[] indices; 
    private Path[] ap;
    private AIPath[] AIPath;
    [SerializeField]
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
        currentCheckpoint = new int[levelContestants];
        //sortedPosition = new string[levelContestants];
        indices = new int[levelContestants];
        
        AI_Controlers = new AI_Controler[levelContestants];
        Checkpoint_Controlers = new Checkpoint_Controler[levelContestants];
        

        player_Character_Controller = GameObject.Find("Player").GetComponent<Player_Character_controller>();
        m_camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        PauseShade = GameObject.Find("PauseShade").GetComponent<PauseShade>();

        for (int i = 0; i < levelContestants; i++)
        {
            if (GameObject.Find("AI Player_0" + i).GetComponent<AIPath>() == null)
            {
                currentCheckpoint[i] = player_Character_Controller.currentCheckpoint;
                levelPlayerLaps[i] = player_Character_Controller.lapNumber;
                
            }

            AIPath[i] = GameObject.Find("AI Player_0" + i).GetComponent<AIPath>();
            AI_Controlers[i] = GameObject.Find("Target_0" + i).GetComponent<AI_Controler>();
            currentCheckpoint[i] = AI_Controlers[i].currentCheckpoint;
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
        for (int i = 0; i < levelContestants; i++) //For each player in a level, do 1 loop
        {
            Vector3 playerPosition;
            Vector3 nextCheckpointPosition;
            int checkpoint = currentCheckpoint[i] - 1;
            if (i != levelContestants)
            {
                nextCheckpointPosition = new Vector3(AI_Controlers[i].transform.position.x, AI_Controlers[i].transform.position.y, AI_Controlers[i].transform.position.z);
                playerPosition = new Vector3(AIPath[i].transform.position.x, AIPath[i].transform.position.y, AIPath[i].transform.position.z);
            }
            else
            {
                Debug.Log("Boowamp...");
                playerPosition = new Vector3(player_Character_Controller.transform.position.x, player_Character_Controller.transform.position.y, player_Character_Controller.transform.position.z);
                nextCheckpointPosition = new Vector3(Checkpoint_Controlers[player_Character_Controller.currentCheckpoint + 1].transform.position.x, Checkpoint_Controlers[player_Character_Controller.currentCheckpoint + 1].transform.position.y, Checkpoint_Controlers[player_Character_Controller.currentCheckpoint + 1].transform.position.z);

            }

            float AbsDistance = Vector3.Distance(nextCheckpointPosition, playerPosition);
            AbsDistance = Mathf.Abs(AbsDistance);
            AbsDistance = AbsDistance-(1000*checkpoint)-(100000 * (levelPlayerLaps[i]-1)) + levelLapsCount*100000+levelCheckpointRollover*1000;
            currentPosition[i] = AbsDistance;
            
        }
        for (int i = 0; i < currentPosition.Length; i++)
        {
            indices[i] = i;
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