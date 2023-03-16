using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerTransparentChecker : MonoBehaviour
{
    public bool isTransparent;
    public bool isInvis;
    public float desiredValue;
    public float currentValue;
    public Tilemap tilemap;
    [SerializeField]
    private Player_Character_controller player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(0.2f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tilemap = GetComponent<Tilemap>();
        player = GameObject.Find("Player").GetComponent<Player_Character_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.layer == 8 && isTransparent == true)
        {
            desiredValue = .50f;
        }
        else if (isInvis == true)
        {
            desiredValue = 0;
        }
        else
        {
            desiredValue = 1f;
        }

        SmoothChange(desiredValue);
    }



    void SmoothChange(float desired)
    {
        currentValue = Mathf.Lerp(currentValue, desired, 0.01f);
        tilemap.color = new Color(1, 1, 1, currentValue);

    }
}

