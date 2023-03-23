using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRefillScript : MonoBehaviour
{
    public int currentItem;
    //can be x possible values
    //0 is holding no Item
    //1 is Hot Pasta
    //2 is Greasy Chilli Dog
    //3 is Tip-Of-Your-Tounge Energy Drink
    //4 is Psychotica
    //5 is ChinChin's Delight.
    public bool isHoldingItem;
    public bool wasHoldingItem;
    public float itemTimer;
    private Rigidbody2D rb2d;
    private Player_Character_controller player;
    private LevelDefine levelDefine;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentItem = Random.Range(0,5);
        if (currentItem == 0)
        {
            wasHoldingItem = true;
            isHoldingItem = false;
        }
        else
        {
            wasHoldingItem = true;
            isHoldingItem = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wasHoldingItem == true && isHoldingItem == false)
        {
            itemTimer = 15f;
            wasHoldingItem = false;
        }
        if (wasHoldingItem == false && isHoldingItem == false)
        {
            itemTimer -= Time.deltaTime;
        }
        if (itemTimer <= 0 && isHoldingItem == false)
        {
            currentItem = Random.Range(1, 5);
        }

        if (currentItem == 0)
        {
            wasHoldingItem = true;
            isHoldingItem = false;
        }
        else
        {
            wasHoldingItem = true;
            isHoldingItem = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player = collision.gameObject.GetComponent<Player_Character_controller>();
            //add giving Item to player
            currentItem = 0;
        }
        if (collision.gameObject.tag == "AIPlayer0" || collision.gameObject.tag == "AIPlayer1" || collision.gameObject.tag == "AIPlayer2" || collision.gameObject.tag == "AIPlayer3")
        {
            
        }
    }



}
