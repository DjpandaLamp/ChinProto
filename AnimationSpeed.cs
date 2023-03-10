using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{
    private Animator m_Animator;
    private LevelDefine characteristics;

    private void Start()
    {
        m_Animator = this.GetComponent<Animator>();
        characteristics = GameObject.Find("LevelDefine").GetComponent<LevelDefine>();
    }
    void Update()
    {
        m_Animator.speed = Mathf.Lerp(characteristics.levelSpeed, m_Animator.speed,  0.2f);
    }
}
