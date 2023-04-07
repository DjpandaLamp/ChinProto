using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WheelDustHandler : MonoBehaviour
{
    public float emissionRate;
    private ParticleSystem particle;
    private ParticleSystem.EmissionModule emission;
    private Player_Character_controller player_;

    // Start is called before the first frame update
    void Start()
    {
        player_ = GetComponentInParent<Player_Character_controller>();
        particle = GetComponent<ParticleSystem>();
        emission = particle.emission;
    }

    // Update is called once per frame
    void Update()
    {
        emissionRate = Vector2.Dot(player_.transform.up, player_.rb.velocity);
        emissionRate = Mathf.Abs(emissionRate);
        emission.rateOverTime = emissionRate*3;
    }
}
