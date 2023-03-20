using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer sp;
    public int layerType;
    public bool isDebug;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
        sp = GetComponent<SpriteRenderer>();
        if (gameObject.name == "LowChangers")
        {
            layerType = 0;
        }
        if (gameObject.name == "MidChangers")
        {
            layerType = 1;
        }
        if (gameObject.name == "HighChangers")
        {
            layerType = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) == true)
        {
            isDebug = true;
        }
        else
        {
            isDebug = false;
        }

        if (isDebug == true && sp != null)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.5f); 
        }
        else if (sp != null)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (layerType == 0)
        {
            if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10) //Low
            {
                collision.gameObject.layer = 8;
                collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 16);
            }
        }
        if (layerType == 1)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9) //Mid
            {
                collision.gameObject.layer = 10;
                collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 14);
            }
        }
        if (layerType == 2)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10) //High
            {
                collision.gameObject.layer = 9;
                collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 12);
            }
        }
    }
}
