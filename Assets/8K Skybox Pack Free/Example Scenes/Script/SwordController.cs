using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine;

public class SwordController : MonoBehaviour
{
    Animator m_Animator;
    MeshRenderer meshRenderer;
    System.Random ran;

    public double GetRandomNumber(double minimum, double maximum, int Len=8)   //Len小数点保留位数
    {
        return Math.Round(ran.NextDouble() * (maximum - minimum) + minimum, Len);
    }

    // Start is called before the first frame update
    void Start()
    {
        ran = new System.Random();
        meshRenderer = GetComponent<MeshRenderer>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_Animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Animator.SetTrigger("AttackUp");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Animator.SetTrigger("Defense");
        }
    }
}
