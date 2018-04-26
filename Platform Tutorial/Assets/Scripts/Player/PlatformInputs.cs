using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInputs : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpHeight = 4.0f;
    public float minimumJumpTime = 2;
    public float attackCooldown = 2;
    public float shotSpeed = 4;
    public int startArrows = 5;
    public Transform groundCheckR;
    public Transform groundCheckL;
    public GameObject jumpParticles;
    public GameObject attack;
    public GameObject arrow;

    private float horizontalDirection;
    private float verticalDirection;
    private float jumpTimer = -1;
    private float attackTimer;
    private int arrows;
    private bool grounded;
    private bool jumped;
    private bool aiming = false;
    private bool rightBool = true;
    private Rigidbody2D rgbd2d;
    private Animator anim;
    private Vector2 aimingDir = Vector2.right;

	void Start ()
    {
        arrows = startArrows;
        rgbd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {

        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        

        if (!aiming)
        {
            transform.Translate(new Vector3(horizontalDirection, 0, 0) * speed * Time.deltaTime);
        }




        grounded = (Physics2D.OverlapPoint(groundCheckR.position) || Physics2D.OverlapPoint(groundCheckL.position)) && rgbd2d.velocity.y < 0.1f;
        if (grounded)
        {
            jumped = false;
            jumpTimer = -1;
        }



        if (Input.GetAxis("AimingController") > 0.1f && arrows > 0)
            Aiming();
        else if (aiming)
            Shot();


        if (Input.GetButtonDown("Attack") && attackTimer < 0)
        {
            Attack();
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


        if (jumpTimer >= 0)
            jumpTimer += Time.deltaTime;

        attackTimer -= Time.deltaTime;

        /*anim.SetFloat("speed", Mathf.Abs(horizontalDirection));
        anim.SetBool("grounded", grounded);*/
	}

    private void FixedUpdate()
    {

    }

    private void Aiming()
    {
        aiming = true;

        if (horizontalDirection != 0 && verticalDirection != 0)
            aimingDir = new Vector2(horizontalDirection, verticalDirection).normalized + new Vector2(0, +GetComponent<BoxCollider2D>().size.y / 2);

        if (transform.GetComponentInChildren<Arrow>() == null)
            Instantiate(arrow, transform, false).transform.localPosition = aimingDir;
        else
            transform.GetComponentInChildren<Arrow>().transform.localPosition = aimingDir;

        transform.GetComponentInChildren<Arrow>().transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up,aimingDir - new Vector2(0, GetComponent<BoxCollider2D>().size.y / 2)),new Vector3(0,0,1));
        

    }

    private void Shot()
    {
        aiming = false;
        arrows--;

        transform.GetComponentInChildren<Arrow>().GetComponent<Rigidbody2D>().AddForce((aimingDir - new Vector2(0, +GetComponent<BoxCollider2D>().size.y / 2) )* shotSpeed,ForceMode2D.Impulse);
        transform.GetComponentInChildren<Arrow>().Shot();

    }

    private void Jump()
    {
        rgbd2d.velocity = new Vector2(rgbd2d.velocity.x, jumpHeight);
        if (!grounded)
        {
            jumped = true;
        }
        jumpTimer = 0;
        grounded = false;
    }

    private void Attack()
    {
        if(rightBool)
        Instantiate(attack, transform, false).transform.localPosition = new Vector2(1, 0.5f);
        else
            Instantiate(attack, transform, false).transform.localPosition = new Vector2(-1, 0.5f);

        attackTimer = attackCooldown;
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
        rightBool = facingRight > 0;

        /*Vector3 myScale = transform.localScale;
        myScale.x = facingRight;
        transform.localScale = myScale;*/
    }
}
