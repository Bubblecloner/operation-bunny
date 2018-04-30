using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInputs : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpHeight = 4.0f;
    public float minimumJumpTime = 2;
    public float attackCooldown = 2;
    public float shotSpeed = 4;
    public float potionDrink = 2;
    public int startArrows = 5;
    public Transform groundCheckR;
    public Transform groundCheckL;
    public GameObject jumpParticles;
    public GameObject attack;
    public GameObject arrow;
    public GameObject shield;
    public LayerMask jumpMask;

    private float horizontalDirection;
    private float verticalDirection;
    private float jumpTimer = -1;
    private float attackTimer;
    private float potionTimer;
    private int arrows;
    private bool grounded;
    private bool jumped;
    private bool aiming = false;
    private bool rightBool = true;
    private bool drunk = false;
    private Rigidbody2D rgbd2d;
    private Animator anim;
    private Vector2 aimingDir = Vector2.right;

    private bool rightTriggerFirstFrame = true;
    public bool Shielding { get; private set; }

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





        //sjekker om spilleren er på bakken
        grounded = ((Physics2D.OverlapPointAll(groundCheckR.position,jumpMask).Length >= 1 && Physics2D.OverlapPointAll(groundCheckR.position,jumpMask)[0].isTrigger == false) || (Physics2D.OverlapPointAll(groundCheckL.position,jumpMask).Length >= 1 && Physics2D.OverlapPointAll(groundCheckL.position,jumpMask)[0].isTrigger == false)) && rgbd2d.velocity.y < 0.1f;
        if (grounded)
        {
            jumped = false;
            jumpTimer = -1;
        }


        if ((Input.GetAxisRaw("AimingController") > 0.1f) && arrows > 0 && !Shielding)
        {
            if (rightTriggerFirstFrame)
            {
                StartAim();
                rightTriggerFirstFrame = false;
            }
            else if (aiming)
                Aiming();
        }
        else if (Input.GetButtonDown("Aiming") && arrows > 0 && !Shielding)
        {
            StartAim();
        }
        else if (Input.GetButton("Aiming") && aiming && arrows > 0 && !Shielding)
            Aiming();
        else if (aiming)
            Shot();
        

        if (Input.GetAxisRaw("AimingController") == 0)
            rightTriggerFirstFrame = true;



        //remove before finishing!!!
        if (Input.GetButtonDown("Cheat"))
        {
            int[] temp = {1,2,3 };
            GetComponent<PotionHandler>().Potions = temp;
        }

        if (Input.GetButtonDown("Cheat2"))
        {
            Debug.Log("nothing");
        }

        
        if (Input.GetButtonDown("Shield") && grounded)
            StartShield();
        else if (Input.GetButton("Shield") && grounded)
            Shield();
        else if (Input.GetButtonUp("Shield") && grounded)
            StopShield();


        if (Input.GetButtonDown("Potion") && !Shielding)
            StartPotion();
        else if (Input.GetButton("Potion") && !Shielding)
            Potion();
        else if (potionTimer > 0 && drunk == false)
        {
            GetComponent<PotionHandler>().SwapPotions();
            drunk = true;
        }


        if (Input.GetButtonDown("Attack") && attackTimer < 0 && !Shielding)
        {
            Attack();
        }


        if (jumped == false && Input.GetButtonDown("Jump") && !Shielding)
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

        potionTimer -= Time.deltaTime;

        /*anim.SetFloat("speed", Mathf.Abs(horizontalDirection));
        anim.SetBool("grounded", grounded);*/
	}

    private void FixedUpdate()
    {
        if (!aiming && !Shielding)
        {
            transform.Translate(new Vector3(horizontalDirection, 0, 0) * speed * Time.deltaTime);
        }
    }

    private void StartShield()
    {
        if (shield)
        {
            Shielding = true;
            if (rightBool)
            {
                shield.transform.localPosition = new Vector2(Mathf.Abs(shield.transform.localPosition.x), shield.transform.localPosition.y);
                shield.transform.localScale = new Vector2(shield.transform.localScale.x, Mathf.Abs(shield.transform.localScale.y));
            }
            else
            {
                shield.transform.localPosition = new Vector2(-Mathf.Abs(shield.transform.localPosition.x), shield.transform.localPosition.y);
                shield.transform.localScale = new Vector2(shield.transform.localScale.x, -Mathf.Abs(shield.transform.localScale.y));
            }
            shield.SetActive(true);

        }

    }

    private void Shield()
    {
        if (shield)
        {
            if (shield.transform.localPosition.x < 0 && rightBool)
            {
                shield.transform.localPosition = new Vector2(Mathf.Abs(shield.transform.localPosition.x), shield.transform.localPosition.y);
                shield.transform.localScale = new Vector2(shield.transform.localScale.x, Mathf.Abs(shield.transform.localScale.y));
            }
            else if (shield.transform.localPosition.x > 0 && !rightBool)
            {
                shield.transform.localPosition = new Vector2(-Mathf.Abs(shield.transform.localPosition.x), shield.transform.localPosition.y);
                shield.transform.localScale = new Vector2(shield.transform.localScale.x, -Mathf.Abs(shield.transform.localScale.y));
            }
        }
    }

    private void StopShield()
    {
        if (shield)
        {

            shield.SetActive(false);
        }
        Shielding = false;
    }

    private void StartPotion()
    {
        drunk = false;
        potionTimer = potionDrink;
    }

    private void Potion()
    {
        if (potionTimer <= 0 && !drunk)
        {
            GetComponent<PotionHandler>().DrinkPotion();
            drunk = true;
        }
    }

    private void StartAim()
    {
        aiming = true;

        
        if (horizontalDirection != 0 || verticalDirection != 0)
            aimingDir = new Vector2(horizontalDirection, verticalDirection).normalized + new Vector2(0, +GetComponent<BoxCollider2D>().size.y / 2);

        Instantiate(arrow, transform, false).transform.localPosition = aimingDir;


        transform.GetComponentInChildren<Arrow>().transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, aimingDir - new Vector2(0, GetComponent<BoxCollider2D>().size.y / 2)), new Vector3(0, 0, 1));
    }

    private void Aiming()
    {

        if (horizontalDirection != 0 || verticalDirection != 0)
            aimingDir = new Vector2(horizontalDirection, verticalDirection).normalized + new Vector2(0, +GetComponent<BoxCollider2D>().size.y / 2);


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

    private void StopShot()
    {
        if (aiming)
        {
            aiming = false;
            Destroy(transform.GetComponentInChildren<Arrow>().gameObject);
        }
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



        StopShot();
    }

    private void Attack()
    {
        if(rightBool)
        Instantiate(attack, transform, false).transform.localPosition = new Vector2(1, 0.5f);
        else
            Instantiate(attack, transform, false).transform.localPosition = new Vector2(-1, 0.5f);

        attackTimer = attackCooldown;



        StopShot();
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
