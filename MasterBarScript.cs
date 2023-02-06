using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterBarScript : MonoBehaviour
{
    public Slider speedSlider;
    public Slider boostCooldown;
    public Gradient gradient;
    public Image fill;
    


    public void setSlider(float Velocity)
    {

        speedSlider.value = Velocity;
        fill.color = gradient.Evaluate(speedSlider.normalizedValue);


    }
    public void FixedUpdate()
    {
        speedSlider.value = Mathf.Lerp(speedSlider.value, 0, 0.05f);
    }

}
