using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformInputs : MonoBehaviour
{

    public float speed = 10.0f;
    public float jumpHeight = 4.0f;
    public float minimumJumpTime = 2;
    public float attackCooldown = 2;
    public float shotSpeed = 4;
    public float potionDrink = 2;
    public float shieldReapairTime = 4;
    public float fastFallSpeed = 3;
    public float slowFallSpeed = 0.5f;
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
    private float shieldTimer;
    private int arrows;
    private bool grounded;
    private bool jumped;
    private bool aiming = false;
    private bool rightBool = true;
    private bool hiddenRightBool;
    private Rigidbody2D rgbd2d;
    private Animator anim;
    private Vector2 aimingDir = Vector2.right;
    private Vector2 aimingDirRaw;
    private Vector2 gravity;

    private bool rightTriggerFirstFrame = true;
    public bool Shielding { get; private set; }
    public bool ShieldActive { private get; set; }

    void Start()
    {
        arrows = startArrows;
        rgbd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        gravity = Physics2D.gravity;

        Invoke("LateStart", 0.01f);
    }
    

    void Update()
    {

        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");

        if (Input.GetButton("Aiming"))
        {
            aimingDirRaw = ((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        }
        else
            aimingDirRaw = new Vector2(horizontalDirection, verticalDirection).normalized;



        if (rgbd2d.velocity.y < 0)
        {
            if (!grounded && verticalDirection > 0.2f)
                Physics2D.gravity = new Vector2(0, gravity.y * slowFallSpeed);
            else if (!grounded && verticalDirection < -0.2f)
                Physics2D.gravity = new Vector2(0, gravity.y * fastFallSpeed);
            else
                Physics2D.gravity = gravity;
        }





        //sjekker om spilleren er på bakken
        grounded = ((Physics2D.OverlapPointAll(groundCheckR.position, jumpMask).Length >= 1 && Physics2D.OverlapPointAll(groundCheckR.position, jumpMask)[0].isTrigger == false) || (Physics2D.OverlapPointAll(groundCheckL.position, jumpMask).Length >= 1 && Physics2D.OverlapPointAll(groundCheckL.position, jumpMask)[0].isTrigger == false)) && rgbd2d.velocity.y < 0.1f;
        if (grounded)
        {
            Physics2D.gravity = gravity;
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





        if (Input.GetButtonDown("Shield") && grounded)
            StartShield();
        else if (Input.GetButton("Shield") && grounded && Shielding)
            Shield();
        else
            StopShield();


        if (Input.GetButtonDown("PotionDrink") && !Shielding)
            Potion();
        else if (Input.GetButtonDown("PotionSwap"))
        {
            GameController.gameControllerInstance.SwapPotions();
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

        shieldTimer -= Time.deltaTime;


        if (shieldTimer <= 0 && shieldTimer > -0.9f)
        {
            FixShield();
            shieldTimer = -1;
        }

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
        if (shield && ShieldActive)
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

    private void FixShield()
    {
        shield.GetComponent<Shield>().Activate();
        shieldTimer = -1;
    }

    private void StartPotion()
    {
        potionTimer = potionDrink;
    }

    private void Potion()
    {
        GetComponent<PotionHandler>().DrinkPotion();
    }

    private void StartAim()
    {
        aiming = true;


        if (aimingDirRaw != Vector2.zero)
            aimingDir = aimingDirRaw + new Vector2(0, +GetComponent<BoxCollider2D>().size.y / 2);

        Instantiate(arrow, transform, false).transform.localPosition = aimingDir;


        transform.GetComponentInChildren<Arrow>().transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, aimingDir - new Vector2(0, GetComponent<BoxCollider2D>().size.y / 2)), new Vector3(0, 0, 1));
    }

    private void Aiming()
    {

        if (aimingDirRaw != Vector2.zero)
            aimingDir = aimingDirRaw + new Vector2(0, +GetComponent<BoxCollider2D>().size.y / 2);


        transform.GetComponentInChildren<Arrow>().transform.localPosition = aimingDir;

        transform.GetComponentInChildren<Arrow>().transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, aimingDir - new Vector2(0, GetComponent<BoxCollider2D>().size.y / 2)), new Vector3(0, 0, 1));


    }

    private void Shot()
    {
        aiming = false;
        arrows--;

        transform.GetComponentInChildren<Arrow>().GetComponent<Rigidbody2D>().AddForce((aimingDir - new Vector2(0, +GetComponent<BoxCollider2D>().size.y / 2)) * shotSpeed, ForceMode2D.Impulse);
        transform.GetComponentInChildren<Arrow>().Shot();


        GameController.gameControllerInstance.arrows = arrows;
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
        Physics2D.gravity = gravity;
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
        if (rightBool)
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
        else if (!grounded)
            Invoke("StopJump", minimumJumpTime - jumpTimer);
    }

    private void Flip(int facingRight)
    {

        hiddenRightBool = facingRight > 0;



        /*Vector3 myScale = transform.localScale;
        myScale.x = facingRight;
        transform.localScale = myScale;*/


        Invoke("EndFlip", 0.2f);
    }

    private void EndFlip()
    {
        rightBool = hiddenRightBool;
    }

    public void StartShieldTimer()
    {
        shieldTimer = shieldReapairTime;
    }

    public void AddArrows(int amount)
    {
        if (arrows + amount > startArrows)
            arrows = startArrows;
        else
            arrows += amount;

        GameController.gameControllerInstance.arrows = arrows;
    }


    private void LateStart()
    {
        GameController.gameControllerInstance.arrows = arrows;
    }
}
