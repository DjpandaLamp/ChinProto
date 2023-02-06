using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    private Player_Character_controller player_Character_Controller;
    private CameraFollow cameraFollow;
    public GameObject Player;
    public Sprite ZeroSpeed;
    public Sprite OneSpeed;
    public Sprite TwoSpeed;
    public Sprite ThreeSpeed;
    public Sprite FourSpeed;


    // Start is called before the first frame update
    void Awake()
    {
        player_Character_Controller = GameObject.Find("Player").GetComponent<Player_Character_controller>();
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
       


        if (Mathf.Abs(player_Character_Controller.velocity.x) < 0.02 && Mathf.Abs(player_Character_Controller.velocity.y) < 0.02)
        {
            GetComponent<SpriteRenderer>().sprite = ZeroSpeed;
        }
        if (Mathf.Abs(player_Character_Controller.velocity.x) > 0.02 || Mathf.Abs(player_Character_Controller.velocity.y) > 0.02)
        {
            GetComponent<SpriteRenderer>().sprite = OneSpeed;
        }
        if (Mathf.Abs(player_Character_Controller.velocity.x) > 0.04 || Mathf.Abs(player_Character_Controller.velocity.y) > 0.04)
        {
            GetComponent<SpriteRenderer>().sprite = TwoSpeed;
        }
        if (Mathf.Abs(player_Character_Controller.velocity.x) > 0.06 || Mathf.Abs(player_Character_Controller.velocity.y) > 0.06)
        {
            GetComponent<SpriteRenderer>().sprite = ThreeSpeed;
        }
        if (Mathf.Abs(player_Character_Controller.velocity.x) >= 0.08 || Mathf.Abs(player_Character_Controller.velocity.y) >= 0.08)
        {
            GetComponent<SpriteRenderer>().sprite = FourSpeed;
        }

    }
}
