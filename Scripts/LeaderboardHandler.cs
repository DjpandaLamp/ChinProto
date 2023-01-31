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

    private void Start()
    {
        characteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefineCharacteristics>();
    }
    SetLeaderBoardItemInfo[] setLeaderBoardItemInfo;
    private void Awake()
    {


        characteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefineCharacteristics>();
        VerticalLayoutGroup leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        setLeaderBoardItemInfo = new SetLeaderBoardItemInfo[characteristics.levelContestants - 1];

        for (int i = 0; i < 4; i++)
        {
            GameObject leaderboardInfoGameobject = Instantiate(leaderboardObjectPrefab[i], leaderboardLayoutGroup.transform);

            setLeaderBoardItemInfo[i] = leaderboardInfoGameobject.GetComponent<SetLeaderBoardItemInfo>();
            // Sprite driveSprite = characteristics.
            // setLeaderBoardItemInfo[i].SetDriverImage();
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
