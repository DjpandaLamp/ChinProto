using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDefine : MonoBehaviour
{
    
    public GameObject AIPlayerPrefab;
    public GameObject AITargetPrefab;
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    
    private GameObject[] AIPlayers;
    private GameObject[] AITargets;
    private GameObject player;
    private GameObject cameraMain;
    private StartLine[] startLine;

    public int count;
    
    // Start is called before the first frame update
    void Start()
    {
        AIPlayers = new GameObject[count-1];
        AITargets = new GameObject[count];
        for (int i=0;i<count;i++)
        {
            AITargets[i] = Instantiate<GameObject>(AITargetPrefab);
            if (AIPlayers.Length >= i)
            {
                AIPlayers[i] = Instantiate<GameObject>(AIPlayerPrefab);
                AIPlayers[i].transform.position = startLine[i].transform.position;
                AIPlayers[i].transform.Rotate(0, 0, startLine[i].transform.eulerAngles.z);
            }
            else
            {
                player = Instantiate<GameObject>(playerPrefab);
                cameraMain = Instantiate<GameObject>(cameraMain);
            }
            

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
