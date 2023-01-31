using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ColorControler : MonoBehaviour
{
    private Animator m_Animator;
    

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        if (gameObject.name == "AI Player_00")
        {
            m_Animator.SetTrigger("purpleDrive");
        }
        if (gameObject.name == "AI Player_01")
            {
                m_Animator.SetTrigger("cyanDrive");
            }
        if (gameObject.name == "AI Player_02")
            {
                m_Animator.SetTrigger("tanDrive");
            }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
