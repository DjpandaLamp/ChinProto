using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAIControler : MonoBehaviour
{
    public enum aIMode { Chase, Node }

    [Header("AI Setting")]
    public aIMode AIMode;

    //Components 
    private PlayerMovement PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
