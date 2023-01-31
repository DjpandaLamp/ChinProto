using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseShade : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float opacity;
    public Color color;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        sprite.color = new Color(color.r, color.g, color.b, opacity);
    }
}
