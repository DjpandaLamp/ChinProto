using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Controler : MonoBehaviour 
{
    public int CheckpointNumber;
    public SpriteRenderer sprite;
    public float opacity = 0;
    public Color color;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        sprite.color = new Color(color.r, color.g, color.b, opacity);
        if (gameObject.name == "Checkpoint_0")
        {
            sprite.color = new Color(color.r, color.g, color.b, 1f);
        }
    }
}
