﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInputs : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpHeight = 4.0f;
    public float minimumJumpTime = 2;
    public Transform groundCheckR;
    public Transform groundCheckL;
    public GameObject jumpParticles;

    private float horizontalDirection;
    private float jumpTimer = -1;
    private bool grounded;
    private bool jumped;
    private Rigidbody2D rgbd2d;
    private Animator anim;

	void Start ()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (jumpTimer >= 0)
            jumpTimer += Time.deltaTime;


        horizontalDirection = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalDirection, 0, 0) * speed * Time.deltaTime);


        grounded = (Physics2D.OverlapPoint(groundCheckR.position) || Physics2D.OverlapPoint(groundCheckL.position)) && rgbd2d.velocity.y < 0.1f;
        if (grounded)
        {
            jumped = false;
            jumpTimer = -1;
        }


        if (jumped == false && Input.GetButtonDown("Jump"))
            Jump();


        //Cuts the jump of the player when the jump button is released
        if (Input.GetButtonUp("Jump") && rgbd2d.velocity.y > 0)
            StopJump();

        if (!Input.GetButton("Jump") && jumpTimer < 0.2f && jumpTimer > 0)
            StopJump();



        if (horizontalDirection > 0)
            Flip(1);
        else if (horizontalDirection < 0)
            Flip(-1);

        /*anim.SetFloat("speed", Mathf.Abs(horizontalDirection));
        anim.SetBool("grounded", grounded);*/
	}

    private void FixedUpdate()
    {

    }

    private void Jump()
    {
        Debug.Log(jumped);
        rgbd2d.velocity = new Vector2(rgbd2d.velocity.x, jumpHeight);
        if (!grounded)
        {
            jumped = true;
        }
        jumpTimer = 0;
        grounded = false;
    }

    private void StopJump()
    {
        if (jumpTimer >= minimumJumpTime)
        {
            rgbd2d.velocity = Vector2.Lerp(rgbd2d.velocity, new Vector2(rgbd2d.velocity.x, 0), 0.8f);
        }
        else if(!grounded)
            Invoke("StopJump", minimumJumpTime - jumpTimer);
    }

    private void Flip(int facingRight)
    {
        Vector3 myScale = transform.localScale;
        myScale.x = facingRight;
        transform.localScale = myScale;
    }
}
