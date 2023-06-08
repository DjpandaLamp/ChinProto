using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement;
    Slider slider;
    float lerpedSlider;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(0.2f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        slider.maxValue = playerMovement.maxSpeed * 1.5f;

        lerpedSlider = Mathf.Lerp(slider.value, playerMovement.velocityVsUp, 0.05f);
        slider.value = lerpedSlider;
    }
}
