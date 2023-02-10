using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class LeaderboardHandler : MonoBehaviour
{
    private LevelDefineCharacteristics characteristics;

    public GameObject[] leaderboardObjectPrefab;
    public Sprite[] leaderboardSprites;
    public SpriteRenderer[] Players;
    public Player_Character_controller humanPlayer;

    private void Start()
    {
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        characteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefineCharacteristics>();
        humanPlayer = GameObject.Find("Player").GetComponent<Player_Character_controller>();
        leaderboardSprites = new Sprite[4];
        Players = new SpriteRenderer[4];
        for (int i = 0; i < 4; i++)
        {
            Players[i] = GameObject.Find("AI Player_0" + i).GetComponent<SpriteRenderer>();
            if (leaderboardSprites[i] == null)
            {
                leaderboardSprites[i] = humanPlayer.p_spriteRenderer.sprite;
                break;
            }
            leaderboardSprites[i] = Players[i].sprite;
            
            
        }

    }

    SetLeaderBoardItemInfo[] setLeaderBoardItemInfo;
    private void Awake()
    {


        characteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefineCharacteristics>();
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        setLeaderBoardItemInfo = new SetLeaderBoardItemInfo[characteristics.levelContestants];

        for (int i = 0; i < characteristics.levelContestants; i++)
        {
            GameObject leaderboardInfoGameobject = Instantiate(leaderboardObjectPrefab[i], leaderboardLayoutGroup.transform);

            setLeaderBoardItemInfo[i] = leaderboardInfoGameobject.GetComponent<SetLeaderBoardItemInfo>();
            // Sprite driveSprite = characteristics.
            // setLeaderBoardItemInfo[i].SetDriverImage();
            UpdateSprites();
        }
    }

    private void Update()
    {
        UpdateSprites();
    }
    void UpdateSprites()
    {
        for (int i = 0; i < characteristics.levelContestants-1; i++)
        {

            setLeaderBoardItemInfo[i].SetDriverImage(leaderboardSprites[characteristics.indices[i]]);
            
        }
    }


}
