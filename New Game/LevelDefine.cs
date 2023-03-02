using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelDefine : MonoBehaviour
{

    public string levelName;
    public int levelLapsCount;
    public int levelContestants;
    public int levelCheckpointRollover;
    public int[] levelPlayerLaps;
    public bool gamePaused = false;
    public bool[] levelPlayerComplete;
    public bool isLevelComplete;
    public float levelSpeed = 1f;
    public float[] levelPlayerTime;
    public float[] distanceToNextCheckpoint;
    public float[] currentPosition;
    public int[] currentCheckpoint;
    public int[] indices;

    public GameObject AIPlayerPrefab;
    public GameObject AITargetPrefab;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public GameObject pauseShadePrefab;

    public GameObject[] aIPlayers;
    public GameObject[] aITargets;
    private GameObject player;
    private GameObject cameraMain;
    private GameObject pauseMain;
    public StartLine[] startLine;

    private PauseShade pauseShade;
    private Checkpoint_Controler[] checkControl;
    private Player_Character_controller playerScript;
    private CameraFollow cameraScript;
    private AIPath[] aIPath;
    private AIDestinationSetter[] aIDest;
    private AI_Controler[] aITargetScript;

    public int count = 5;

    // Start is called before the first frame update
    void Start()
    {
        pauseMain = Instantiate<GameObject>(pauseShadePrefab);
        pauseShade = pauseMain.GetComponent<PauseShade>();

        indices = new int[levelContestants];
        currentCheckpoint = new int[levelContestants];
        levelPlayerLaps = new int[levelContestants];

        levelPlayerComplete = new bool[levelContestants];

        levelPlayerTime = new float[levelContestants];
        distanceToNextCheckpoint = new float[levelContestants];
        currentPosition = new float[levelContestants];




        aITargetScript = new AI_Controler[count];
        startLine = new StartLine[count];
        aIPlayers = new GameObject[count-1];
        aIPath = new AIPath[count];
        aIDest = new AIDestinationSetter[count];
        aITargets = new GameObject[count];

        StartCoroutine(LateStart(0.1f));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i=0;i<count;i++)
        {
            aITargets[i] = Instantiate<GameObject>(AITargetPrefab);
            aITargets[i].name = "Target_0" + i;
            
            aITargetScript[i] = aITargets[i].GetComponent<AI_Controler>();
            if (aIPlayers.Length >= i)
            {
                aIPlayers[i] = Instantiate<GameObject>(AIPlayerPrefab);
                aIPlayers[i].transform.position = startLine[i].transform.position;
                aIPlayers[i].transform.Rotate(0, 0, startLine[i].transform.eulerAngles.z);
                
                aIPath[i] = aIPlayers[i].GetComponent<AIPath>();
                aIDest[i] = aIPlayers[i].GetComponent<AIDestinationSetter>();
                aIDest[i].target = aITargets[i].transform;
            }
            else
            {
                player = Instantiate<GameObject>(playerPrefab);
                cameraMain = Instantiate<GameObject>(cameraPrefab);
                playerScript = player.GetComponent<Player_Character_controller>();
                cameraScript = cameraMain.GetComponent<CameraFollow>();
                player.transform.position = startLine[i].transform.position;
                player.transform.Rotate(0, 0, startLine[i].transform.eulerAngles.z);
                cameraScript.Target = player.transform;
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLapPlayer()
    {
        if (playerScript.currentCheckpoint > levelCheckpointRollover)
        {
            playerScript.currentCheckpoint = 0;
            playerScript.lapNumber += 1;
            levelPlayerLaps[levelContestants] = playerScript.lapNumber;
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
                else if (levelPlayerComplete[i] && aIPath[i] == null)
                {
                    var NewAIPlayer = Instantiate(AIPlayerPrefab, playerScript.transform.position, playerScript.transform.rotation); //Spawns AI Player at the players position with the players rotation
                    playerScript.gameObject.SetActive(false); //Disables the player
                    NewAIPlayer.name = "AI Player_0" + i;
                    NewAIPlayer.tag = "AIPlayer" + i;
                    aIPath[i] = NewAIPlayer.GetComponent<AIPath>();
                    var _Dest = NewAIPlayer.GetComponent<AIDestinationSetter>();
                    _Dest.target = aITargets[i].transform;
                    cameraScript.Target = NewAIPlayer.transform;
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
            int checkpoint = currentCheckpoint[i];
            if (i != levelContestants - 1)
            {
                Debug.Log("WEEE WOOOO...");
                nextCheckpointPosition = new Vector3(aITargets[i].transform.position.x, aITargets[i].transform.position.y, aITargets[i].transform.position.z);
                playerPosition = new Vector3(aITargets[i].transform.position.x, aITargets[i].transform.position.y, aITargets[i].transform.position.z);

            }
            else
            {
                Debug.Log("Boowamp...");
                playerPosition = new Vector3(playerScript.transform.position.x, playerScript.transform.position.y, playerScript.transform.position.z);
                if (playerScript.currentCheckpoint + 1 > checkControl.Length)
                {
                    nextCheckpointPosition = new Vector3(checkControl[0].transform.position.x, aITargets[0].transform.position.y, aITargets[0].transform.position.z);
                }
                else
                {
                    nextCheckpointPosition = new Vector3(checkControl[playerScript.currentCheckpoint + 1].transform.position.x, checkControl[playerScript.currentCheckpoint + 1].transform.position.y, checkControl[playerScript.currentCheckpoint + 1].transform.position.z);
                }



            }

            float AbsDistance = Vector3.Distance(nextCheckpointPosition, playerPosition);

            AbsDistance = AbsDistance - (1000 * checkpoint) - (100000 * (levelPlayerLaps[i] - 1)) + levelLapsCount * 100000 + levelCheckpointRollover * 1000;
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
        playerScript.moneyText.text = "x" + playerScript.Money.ToString();
        playerScript.lapText.text = "Laps: " + playerScript.lapNumber.ToString() + "/" + levelLapsCount.ToString();

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

            pauseShade.opacity = Mathf.Lerp(pauseShade.opacity, 0.5f, 0.01f);
            for (int i = 0; i < levelContestants; i++)
            {
                if (aIPath[i] == null)
                {
                    continue;
                }
                aIPath[i].canMove = false;
            }


        }
        else
        {
            levelSpeed = 1;

            pauseShade.opacity = Mathf.Lerp(pauseShade.opacity, 0, 0.01f);
            for (int i = 0; i < levelContestants; i++)
            {
                if (aIPath[i] == null)
                {
                    continue;
                }
                aIPath[i].canMove = true;
            }

        }

        if (playerScript != null)
        {
            playerScript.playerSpeed = levelSpeed;
        }


    }




    void UpdateData()
    {
        for (int i = 0; i < levelContestants; i++)
        {
            if (aITargets[i] != null)
            {
                currentCheckpoint[i] = aITargetScript[i].currentCheckpoint;
            }
            else
            {
                currentCheckpoint[i] = playerScript.currentCheckpoint;
            }
        }
    }
}
