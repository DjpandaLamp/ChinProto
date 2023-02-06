using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public float currentMoney;
    public int currentCoins;
    public int currentStory;

    public string[] levelName;
    public string[] levelID;
    public bool[] hasPlayed;
    public bool[] isReplayable;
    public int[] levelLapsCount;
    public int[] levelContestants;
    public int[] levelCheckpointRollover;
    public float[] levelRewards;
    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        DefineLevels();


    }
    void DefineLevels()
    {
        levelName = new string[86];
        levelID = new string[86];
        levelRewards = new float[86];
        levelLapsCount = new int[86];
        levelContestants = new int[86];
        levelCheckpointRollover = new int[86];
        hasPlayed = new bool[86];
        isReplayable = new bool[86];

        levelName[0] = "Tutorial Level"; //tutorial
        levelID[0] = "aTutorial";
        levelRewards[0] = 0f;
        levelLapsCount[0] = 2;
        levelContestants[0] = 1;
        levelCheckpointRollover[0] = 3;
        hasPlayed[0] = false;
        isReplayable[0] = true;

        levelName[1] = "Turnip Screenpike 1"; //Rookie Cup Match 1
        levelID[1] = "a00";
        levelRewards[1] = 10f;
        levelLapsCount[1] = 3;
        levelContestants[1] = 3;
        levelCheckpointRollover[1] = 3;
        hasPlayed[1] = false;
        isReplayable[1] = false;

        levelName[2] = "Turnip Screenpike 2"; //Rookie Cup Match 2
        levelID[2] = "a01";
        levelRewards[2] = 15f;
        levelLapsCount[2] = 3;
        levelContestants[2] = 4;
        levelCheckpointRollover[2] = 3;
        hasPlayed[2] = false;
        isReplayable[2] = false;

        levelName[3] = "Placeholder_Level_Name1"; //Rookie Cup Match 3
        levelID[3] = "a02";
        levelRewards[3] = 60f;
        levelLapsCount[3] = 3;
        levelContestants[3] = 4;
        levelCheckpointRollover[3] = 3;
        hasPlayed[3] = false;
        isReplayable[3] = false;

        levelName[4] = "Placeholder_Level_Name1"; //Rookie Cup Match 4
        levelID[4] = "a03";
        levelRewards[4] = 60f;
        levelLapsCount[4] = 3;
        levelContestants[4] = 4;
        levelCheckpointRollover[4] = 3;
        hasPlayed[4] = false;
        isReplayable[4] = false;

        levelName[5] = "Placeholder_Level_Name2"; //Rookie Cup Match 5
        levelID[5] = "a04";
        levelRewards[5] = 50f;
        levelLapsCount[5] = 3;
        levelContestants[5] = 4;
        levelCheckpointRollover[5] = 4;
        hasPlayed[5] = false;
        isReplayable[5] = false;

        levelName[6] = "Placeholder_Level_Name2"; //Rookie Cup Match 6
        levelID[6] = "a05";
        levelRewards[6] = 15f;
        levelLapsCount[6] = 3;
        levelContestants[6] = 4;
        levelCheckpointRollover[6] = 3;
        hasPlayed[6] = false;
        isReplayable[6] = false;

        levelName[7] = "Placeholder_Level_Name3"; //Rookie Cup Pre-Quarter Finals
        levelID[7] = "a06";
        levelRewards[7] = 60f;
        levelLapsCount[7] = 3;
        levelContestants[7] = 4;
        levelCheckpointRollover[7] = 3;
        hasPlayed[7] = false;
        isReplayable[7] = false;

        levelName[8] = "Placeholder_Level_Name3"; //Rookie Cup Quarter Finals
        levelID[8] = "a07";
        levelRewards[8] = 60f;
        levelLapsCount[8] = 3;
        levelContestants[8] = 4;
        levelCheckpointRollover[8] = 3;
        hasPlayed[8] = false;
        isReplayable[8] = false;

        levelName[9] = "Placeholder_Level_Name4"; //Rookie Cup Semi-Finals
        levelID[9] = "a08";
        levelRewards[9] = 60f;
        levelLapsCount[9] = 3;
        levelContestants[9] = 4;
        levelCheckpointRollover[9] = 3;
        hasPlayed[9] = false;
        isReplayable[9] = false;

        levelName[10] = "Placeholder_Level_Name5"; //Rookie Cup Finals
        levelID[10] = "a09";
        levelRewards[10] = 60f;
        levelLapsCount[10] = 3;
        levelContestants[10] = 4;
        levelCheckpointRollover[10] = 3;
        hasPlayed[10] = false;
        isReplayable[10] = true;


    }
}
