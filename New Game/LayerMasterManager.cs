using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LayerMasterManager : MonoBehaviour
{
    private Player_Character_controller player;

    public LayerTransparentChecker[] layers;
    public int taggedWithLayer;

    public string obstHighLayer = "TilesHighToggleable_0";
    public string obstMid1Layer = "TilesMidToggleable-Tunnel_1";
    public string obstMid2Layer = "TilesMidToggleable-Bridge_2";
    public string obstLowLayer = "TilesLowToggleable_3";
    public string obstUnderLayer = "TilesUnderToggleable_4";
    public string transHighLayer = "TilesHighTrans_5";
    public string transMidLayer = "TilesMidTrans_6";
    public string transLowLayer = "TilesLowTrans_7";
    public string invisLowLayer = "TilesLowInvisColider_8";
    public string transMidLayeDecor = "TilesMidTransDecor_9";


    // Start is called before the first frame update
    void Start()
    {
      

        taggedWithLayer = GameObject.FindGameObjectsWithTag("Layer").Length;
        
        layers = new LayerTransparentChecker[taggedWithLayer];

        layers[0] = GameObject.Find(obstHighLayer).GetComponent<LayerTransparentChecker>();
        layers[1] = GameObject.Find(obstMid1Layer).GetComponent<LayerTransparentChecker>();
        layers[2] = GameObject.Find(obstMid2Layer).GetComponent<LayerTransparentChecker>();
        layers[3] = GameObject.Find(obstLowLayer).GetComponent<LayerTransparentChecker>();
        layers[4] = GameObject.Find(obstUnderLayer).GetComponent<LayerTransparentChecker>();
        layers[5] = GameObject.Find(transHighLayer).GetComponent<LayerTransparentChecker>();
        layers[6] = GameObject.Find(transMidLayer).GetComponent<LayerTransparentChecker>();
        layers[7] = GameObject.Find(transLowLayer).GetComponent<LayerTransparentChecker>();
        layers[8] = GameObject.Find(invisLowLayer).GetComponent<LayerTransparentChecker>();
        layers[9] = GameObject.Find(transMidLayeDecor).GetComponent<LayerTransparentChecker>();


        StartCoroutine(LateStart(0.2f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Character_controller>();

    }


    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.layer == 8) //LowCase
        {
            layers[0].desiredValue = 1;
            layers[1].desiredValue = 0;
            layers[2].desiredValue = 0;
            layers[3].desiredValue = 1;
            layers[4].desiredValue = 1;
            layers[5].desiredValue = 0;
            layers[6].desiredValue = 0.5f;
            layers[7].desiredValue = 0;
            layers[8].desiredValue = 0;
            layers[9].desiredValue = 0.5f;
        }
        if (player.gameObject.layer == 9) //High Case
        {
            layers[0].desiredValue = 1;
            layers[1].desiredValue = 1;
            layers[2].desiredValue = 0;
            layers[3].desiredValue = 0;
            layers[4].desiredValue = 0;
            layers[5].desiredValue = 0;
            layers[6].desiredValue = 1;
            layers[7].desiredValue = 0;
            layers[8].desiredValue = 0;
            layers[9].desiredValue = 1f;
        }
        if (player.gameObject.layer == 10) //Mid Case
        {
            layers[0].desiredValue = 1;
            layers[1].desiredValue = 1;
            layers[2].desiredValue = 0;
            layers[3].desiredValue = 0;
            layers[4].desiredValue = 0;
            layers[5].desiredValue = 0;
            layers[6].desiredValue = 1;
            layers[7].desiredValue = 0;
            layers[8].desiredValue = 0;
            layers[9].desiredValue = 1;
        }
        if (player.gameObject.layer == 14)//Undercase
        {
            layers[0].desiredValue = 1;
            layers[1].desiredValue = 0;
            layers[2].desiredValue = 0;
            layers[3].desiredValue = 1;
            layers[4].desiredValue = 1;
            layers[5].desiredValue = 0;
            layers[6].desiredValue = 0;
            layers[7].desiredValue = 0;
            layers[8].desiredValue = 0;
            layers[9].desiredValue = 0.5f;
        }

    }

    void FindGameObjectWithLayer()
    {

        foreach (var layer in layers)
        {

        }
    }
}
