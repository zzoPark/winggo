using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float m_StartXSpeed = 1f;
    [SerializeField] private float m_RightForce = 1f;
    [SerializeField] private float m_MaxYSpeed = 30f;
    [SerializeField] private float m_UpForce = 400f;
    [SerializeField] private float m_FallMultiplier = 2.5f;
    [SerializeField] private LayerMask m_WhatIsObstacle;    // A mask determining what is obstacle

    private bool m_Wing = false;
    private bool m_Down = true;
    private bool m_Crashed = false;
    
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.velocity = new Vector2(m_StartXSpeed, 0);
    }

    private void Update()
    {
        m_Wing = true;
        m_Crashed = false;

        if (m_Rigidbody2D.velocity.y < 0)
        {
            m_Down = true;
        }
        else
        {
            m_Down = false;
        }

        m_Animator.SetBool("Wing", m_Wing);
        m_Animator.SetBool("Down", m_Down);
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Up"))
        {
            if (m_Rigidbody2D.velocity.y < m_MaxYSpeed)
            {
                m_Rigidbody2D.AddForce(Vector2.up * m_UpForce);
            }
        }

        if (m_Rigidbody2D.velocity.y < 0)
        {
            m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallMultiplier - 1) * Time.deltaTime;
        }

        m_Rigidbody2D.AddForce(Vector2.right * m_RightForce);
    }
}
