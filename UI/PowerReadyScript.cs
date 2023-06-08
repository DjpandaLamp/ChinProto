using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerReadyScript : MonoBehaviour
{
    PlayerMovement player_;
 
    SpriteRenderer spriteRenderer;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_.MultSpeed <= 1.1)
        {
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0);
        }
    }
}
