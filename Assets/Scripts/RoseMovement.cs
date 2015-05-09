using UnityEngine;
using System;
using System.Text;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public delegate bool SetControlHandler();  

public class RoseMovement : MonoBehaviour {

    [SerializeField]
    private LayerMask m_WhatIsGround;   // A mask determining what is ground to the character
    [SerializeField]
    private float m_JumpForce = 400f;   // Amount of force added when the player jumps.
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Anim;
    private SetControlHandler setControl;
    private bool m_Jump;
    public float autoSpeed;
    public bool startAtTheStart;

    private void Awake()
    {
        
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();

        if (startAtTheStart)
        {
            this.transform.position = new Vector3(-60.66f, 8.5f, 0);
        }

        #if UNITY_EDITOR
            setControl = () => { return CrossPlatformInputManager.GetButtonDown("Jump"); };
        #elif UNITY_WEBPLAYER
            setControl = () => { return CrossPlatformInputManager.GetButtonDown("Jump");};
        #else
            setControl = () => { return (Input.GetTouch(0).phase == TouchPhase.Began);};
        #endif
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = setControl();
        }
    }


    private void FixedUpdate()
    {
        float h = Time.deltaTime * autoSpeed;

        if (isGrounded)
        {
            m_Rigidbody2D.velocity = new Vector2(h * 100f, m_Rigidbody2D.velocity.y);
        }

        Move(h,false,m_Jump);
        Grounded();        
    }

    private void Move(float move, bool slide, bool jump)
    {
        if (jump && isGrounded)
        {
            m_Anim.SetBool("Jump", true);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void Grounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        isGrounded = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                m_Jump = false;
            }
        }
        
        print("On the ground: " + isGrounded);
        m_Anim.SetBool("Jump", !isGrounded);
    }
}
