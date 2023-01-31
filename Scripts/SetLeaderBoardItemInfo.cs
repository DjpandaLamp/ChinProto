using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLeaderBoardItemInfo : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite DriverSprite;

    void Start()
    {
        
    }
    public void SetDriverImage(Sprite newSprite)
    {
        DriverSprite = newSprite;
        spriteRenderer.sprite = DriverSprite;
    }
}
