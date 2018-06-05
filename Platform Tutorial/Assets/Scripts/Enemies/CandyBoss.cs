using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBoss : Entity {

    public GameObject player;

    public int[] scalingHealth;
    public float speed;
    public float attackCooldown = 1;
    public float triggerRange = 2;
    public float attackDelay = 0.1f;
    public Transform fallCheck;
    public Transform frontCheck;
    public LayerMask wallMask;


    protected AudioSource audioSrcs;
    public AudioClip attackSound;
    public AudioClip specialAttack;
    public AudioClip start;
    public AudioClip takeDmg;
    public AudioClip death;


    public Vector2 attackSize;
    public GameObject attack;
    public GameObject shockWave;
    public Transform[] landingSpots;
    protected Transform targetSpot;
    protected Animator anim;

    protected bool facingRight = false;
    protected float specialTimer;
    protected float attackTimer;
    protected bool grounded;
    protected int performedSpecials = 0;

    protected int fightPhase = 1;
    //0 idle, 1 chasing player,

    protected int random;



    protected override void Start()
    {
        audioSrcs = GetComponent<AudioSource>();
        Play(start);
        maxHealth = scalingHealth[CarryOverInfo.carryOverInfoInstance.upgrades[1]];
        base.Start();
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        anim.SetInteger("Fight Phase", fightPhase);
        anim.SetFloat("Vertical Velocity",rgbd2d.velocity.y);
        grounded = ((Physics2D.OverlapPointAll(fallCheck.position, wallMask).Length >= 1) || (Physics2D.OverlapPointAll(fallCheck.position, wallMask).Length >= 1)) && rgbd2d.velocity.y < 0.1f;
        anim.SetBool("Grounded", grounded);


        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            SpecialStart();
        }

            attackTimer -= Time.deltaTime;


        switch (fightPhase)
        {
            case 0:

                break;

            case 1:
                if ((float)health/maxHealth < (float)(5 - performedSpecials) / 6)
                {
                    SpecialStart();
                    break;
                }

                FaceEnemy(player.transform.position);

                if (facingRight)
                    transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                else
                    transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

                if ((!Physics2D.OverlapPoint(fallCheck.position, wallMask) || Physics2D.OverlapPoint(frontCheck.position, wallMask)) && grounded)
                {
                    SmallJump();
                }
                break;

            case 2:
                rgbd2d.velocity = new Vector2(rgbd2d.velocity.x, 5 - 2*(transform.position.y - landingSpots[1].position.y));
                if(transform.position.y > landingSpots[1].position.y)
                {
                    rgbd2d.gravityScale = 0;
                    rgbd2d.velocity = Vector2.zero;
                    anim.SetBool("Special", true);
                    fightPhase = 3;
                    random = Random.Range(0, landingSpots.Length);
                    targetSpot = landingSpots[random];
                }
                break;

            case 3:

                if (targetSpot.position.x > transform.position.x)
                    rgbd2d.velocity = new Vector2(6*(targetSpot.position.x - transform.position.x), rgbd2d.velocity.y);
                else
                    rgbd2d.velocity = new Vector2(-6*(transform.position.x - targetSpot.position.x), rgbd2d.velocity.y);

                if(Mathf.Abs(Mathf.Abs(targetSpot.position.x)-Mathf.Abs(transform.position.x)) < 0.1f)
                {
                    if (specialTimer > 1)
                    {

                        int temp = Random.Range(0, landingSpots.Length);
                        while (temp == random)
                        {
                            temp = Random.Range(0, landingSpots.Length);
                        }
                        random = temp;


                        targetSpot = landingSpots[random];
                    }
                    else
                    {
                        Play(specialAttack);
                        fightPhase = 5;
                        break;
                    }
                }
                specialTimer -= Time.deltaTime;
                break;

            case 4:
                rgbd2d.velocity = new Vector2(0,-20);

                if (grounded)
                {
                    SpecialSmash();
                    SpecialEnd();
                }


                break;

            case 5:

                if (specialTimer < 0)
                {
                    fightPhase = 4;
                    break;
                }


                specialTimer -= Time.deltaTime;
                break;
        }
    }

    protected void SmallJump()
    {
        rgbd2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
    }

    protected void SpecialStart()
    {
        performedSpecials++;
        damageTimer = 999;
        specialTimer = 6;
        fightPhase = 2;
    }

    protected void SpecialSmash()
    {
        Instantiate(shockWave).transform.position = (Vector2)transform.position + new Vector2(1, 0);
        GameObject temp = Instantiate(shockWave);
        temp.transform.position = (Vector2)transform.position + new Vector2(-1, 0);
        temp.transform.localScale = temp.transform.localScale * new Vector2(-1,1);
    }

    protected void SpecialEnd()
    {
        damageTimer = 0;
        rgbd2d.gravityScale = 1;
        fightPhase = 1;
        anim.SetBool("Special", false);
    }


    protected virtual void FaceEnemy(Vector2 target)
    {
        if (health > 0)
        {
            if (Vector2.Distance(transform.position, target) < triggerRange && attackTimer < 0)
            {
                Invoke("Attack", attackDelay);

                attackTimer = attackCooldown;
            }



            if (target.x > transform.position.x ^ facingRight)
                Flip();


        }
    }

    protected virtual void Attack()
    {
        GameObject temp = Instantiate(attack, transform, false);
        temp.transform.localScale = attackSize;

        if (facingRight)
            temp.transform.localPosition = new Vector2(1, 0.5f);
        else
            temp.transform.localPosition = new Vector2(-1, 0.5f);

        temp.GetComponent<Attack>().targetTag = "Player";

        Play(attackSound);
    }

    protected virtual void Flip()
    {
        GetComponentInChildren<SpriteRenderer>().transform.localScale *= new Vector2(-1,1);

        facingRight = !facingRight;
        fallCheck.localPosition = new Vector2(-fallCheck.localPosition.x, fallCheck.localPosition.y);
        frontCheck.localPosition = new Vector2(-frontCheck.localPosition.x, frontCheck.localPosition.y);
    }

    public override void Die()
    {
        base.Die();
        Play(death);
        Invoke("FinishLevel", 3);
    }

    protected void FinishLevel()
    {

        GameController.gameControllerInstance.ClearLevel();
    }

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        Play(takeDmg);
        base.Harm(dmg, knockBack, knockUp, source);
    }

    private void Play(AudioClip clip)
    {

        audioSrcs.Stop();
        audioSrcs.PlayOneShot(clip);
    }
}
