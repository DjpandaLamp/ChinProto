using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WheelDustHandler : MonoBehaviour
{
    public float emissionRate;
    private ParticleSystem particle;
    private ParticleSystem.EmissionModule emission;
    private Player_Character_controller player_;
    private PlayerMovement playerMovement;
    private Rigidbody2D Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        player_ = GetComponentInParent<Player_Character_controller>();
        if (player_ == null)
        {
            playerMovement = GetComponentInParent<PlayerMovement>();
            Rigidbody2D = GetComponentInParent<Rigidbody2D>();
        }
        particle = GetComponent<ParticleSystem>();
        emission = particle.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_ != null)
        {
            emissionRate = Vector2.Dot(player_.transform.up, player_.rb.velocity);
            emissionRate = Mathf.Abs(emissionRate);
            emission.rateOverTime = emissionRate * 3;
        }
        else
        {
            emissionRate = Vector2.Dot(playerMovement.transform.up, Rigidbody2D.velocity);
            emissionRate = Mathf.Abs(emissionRate);
            emission.rateOverTime = emissionRate * 3;
        }

    }
}
