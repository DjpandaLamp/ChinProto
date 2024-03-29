using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NewAIControler;

public class PowerUpManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer p_sprite;
    public enum mode
    {
        Player,
        AI,
    }

    public mode Mode;

    public float aIPowerRandom;


    [Header("AI Power Chance Ranges")]
    public float nothingPowerChance = 49;
    public float speedPowerChance = 2;

    public float totalChance;
    //Displayed Percent Chance
    public int nothingPowerPercentChance;
    public int speedPowerPercentChance;
    public float totalPercent;

    public int[] chanceArray;


    public int rand;
    public int chosenPowerUp;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        p_sprite = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        switch (Mode)
        {
            case mode.AI:
                AIMode();
                break;

            case mode.Player:
                Player();
                break;

        }
    }



    void AIMode()
    {
        totalChance = nothingPowerChance + speedPowerChance;
        nothingPowerPercentChance = (int)Mathf.Round(100 * (nothingPowerChance / totalChance));
        speedPowerPercentChance = (int)Mathf.Round(100 * (speedPowerChance / totalChance));

        totalPercent = nothingPowerPercentChance + speedPowerPercentChance; //Add all together to create a total value that should equal 100 if all is well. 
        chanceArray = new int[100];

        for (int i = 0; i <= nothingPowerPercentChance; i++)
        {
            chanceArray[i] = 0;
        }
        for (int i = nothingPowerPercentChance; i <= nothingPowerPercentChance + speedPowerPercentChance && i < chanceArray.Length ; i++)
        {


            chanceArray[i] = 1;
        }
        rand = Random.Range(0, 99);
        chosenPowerUp = chanceArray[rand];


        switch(chosenPowerUp)
        {
            case 0:
                {
                    playerMovement.MultSpeed = Mathf.Lerp(playerMovement.MultSpeed, 1.0f, 0.05f);

                    break;
                }

            case 1:
                {
                    playerMovement.MultSpeed = 5f;
                    break;
                }
        }

        if (playerMovement.MultSpeed >= 1.45f)
        {
            p_sprite.color = new Color(0f, 1f, 1, 1);
        }
        else
        {
            p_sprite.color = new Color(1, 1, 1, 1);
        }
    }
    void Player()
    {
      
    }

}
