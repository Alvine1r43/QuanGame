using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;

public class SwordController : MonoBehaviour
{
    Animator m_Animator;
    System.Random ran;
    // Start is called before the first frame update
    void Start()
    {
        System.Random ran = new System.Random();
        
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetFloat("RandomIdle", (float)ran.NextDouble());
    }
}
