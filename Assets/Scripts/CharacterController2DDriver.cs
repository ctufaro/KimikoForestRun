﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public delegate bool SetTouchControlHandler();

public class CharacterController2DDriver : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
    public bool forceStop = false;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
    private SetTouchControlHandler setControl;
    private bool boost;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;

        //toggle between Keyboard and Phone
        #if UNITY_EDITOR
            setControl = () => { return Input.GetKeyDown(KeyCode.Space); };
        #elif UNITY_WEBPLAYER
            setControl = () => { Input.GetKeyDown( KeyCode.Space );};
        #else
            setControl = () => { return Input.GetKeyDown( KeyCode.Space );};
			//setControl = () => { return (Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began);};
        #endif
    }


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		//Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		//Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
        if (forceStop)
        {
            _animator.Play(Animator.StringToHash("Idle"));
            return;
        }

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( _controller.isGrounded )
			_velocity.y = 0;

		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
            //normalizedHorizontalSpeed = 0;

            //if( _controller.isGrounded )
            //    _animator.Play( Animator.StringToHash( "Idle" ) );

            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Run"));
		}


		// we can only jump whilst grounded
		//if( _controller.isGrounded && Input.GetKeyDown( KeyCode.Space ) )
		if( _controller.isGrounded && setControl() && !EventSystem.current.IsPointerOverGameObject())
        {
            Jump();
		}


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

        if (forceStop)
        {
            _velocity.x = 0;
        }
        else
        {
            _controller.move(_velocity * Time.deltaTime);
        }


	}

    public void Go()
    {
        forceStop = false;
        GameObject go = GameObject.FindGameObjectWithTag("MessagePanel");
        go.SetActive(false);
    }

    public void GoAd()
    {
        forceStop = false;
        GameObject go = GameObject.Find("AdPanel1");
        go.SetActive(false);
        boost = true;        
    }

    public void GoAd2()
    {
        forceStop = false;
        GameObject go = GameObject.Find("AdPanel2");
        go.SetActive(false);
        boost = true;
    }

    public void Jump()
    {
        if (boost)
        {
            jumpHeight = 2.3f;
            _velocity.x = 25f;
            boost = false;
        }
        else
        {
            jumpHeight = 1.9f;
        }
        
        _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
        _animator.Play(Animator.StringToHash("Jump"));

        
    }

}
