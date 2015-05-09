using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    public delegate bool SetControlHandler();    
    
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private Rigidbody2D m_Rigidbody2D;
        private SetControlHandler setControl;
        private bool m_Jump;
        public float autoSpeed;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            #if UNITY_EDITOR
                setControl = () => { return CrossPlatformInputManager.GetButtonDown("Jump");};
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
            // Read the inputs.
            //bool crouch = Input.GetKey(KeyCode.LeftControl);
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float h = Time.deltaTime * autoSpeed;
            // Pass all parameters to the character control script.
            m_Character.Move(h, false, m_Jump);
            m_Jump = false;

            //m_Rigidbody2D.velocity = new Vector2(h * 10f, m_Rigidbody2D.velocity.y);
        }

    
    }
}
