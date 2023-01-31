using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLeaderBoardItemInfo : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite DriverSprite;

    private void Update()
    {
        SetDriverImage(DriverSprite);
    }
    public void SetDriverImage(Sprite newSprite)
    {
        DriverSprite = newSprite;
        spriteRenderer.sprite = DriverSprite;
    }
}
