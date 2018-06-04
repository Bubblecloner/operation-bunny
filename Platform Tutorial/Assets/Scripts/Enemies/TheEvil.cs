using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEvil : Entity
{

    public float speed;
    public float attackCooldown = 1;
    public float triggerRange = 2;
    public float attackDelay = 0.1f;
    public Vector2 attackSize;
    public GameObject attack;
    public Transform fallCheck;
    public Transform frontCheck;
    public LayerMask wallMask;


    protected bool facingRight = true;
    protected float attackTimer;



    protected override void Update()
    {
        base.Update();

        if (stunTimer < 0)
        {
            if (GetComponentInChildren<IsAggro>().Aggro)
            {
                FaceEnemy(GameObject.FindGameObjectWithTag("Player").transform.position);

            }






            if (facingRight)
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            else
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        }

        if (!Physics2D.OverlapPoint(fallCheck.position, wallMask) || Physics2D.OverlapPoint(frontCheck.position, wallMask))
        {
            Flip();
        }

        attackTimer -= Time.deltaTime;
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


    }

    protected virtual void Flip()
    {
        if (GetComponentInChildren<IsAggro>().Aggro)
            GetComponentInChildren<IsAggro>().FailedToReach();


        facingRight = !facingRight;
        fallCheck.localPosition = new Vector2(-fallCheck.localPosition.x, fallCheck.localPosition.y);
        frontCheck.localPosition = new Vector2(-frontCheck.localPosition.x, frontCheck.localPosition.y);
    }
}
