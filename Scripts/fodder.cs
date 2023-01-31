using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fodder : MonoBehaviour
{
    [SerializeField]
    public float MovementSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Get Horizontal Input
        float PlayerDirectionX = (Input.GetAxis("Horizontal"));
        //Get Vertical Input
        float PlayerDirectionY = (Input.GetAxis("Vertical"));

        //update position
        Vector3 direction = new Vector3(PlayerDirectionX, PlayerDirectionY, 0).normalized;
        transform.Translate(direction * MovementSpeed * Time.deltaTime);



    }

}