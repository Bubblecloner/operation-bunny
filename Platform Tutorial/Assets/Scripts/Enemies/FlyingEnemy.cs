using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Entity
{

    public float speed = 3;
    public float knockback = 80;

    protected bool aggro;
    protected Vector2 startPosition;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        aggro = GetComponentInChildren<IsAggro>().Aggro;
        if (stunTimer < 0)
            if (aggro)
            {
                MoveTowardTarget(GameObject.FindGameObjectWithTag("Player").transform.position);
            }
            else
                Idle();

    }

    protected virtual void MoveTowardTarget(Vector2 target)
    {
        if (health > 0)
        {


            Vector2 vector = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized * speed;

            transform.Translate(vector);
        }
    }

    protected virtual void Idle()
    {
        if (Vector2.Distance(transform.position, startPosition) > 1)
            MoveTowardTarget(startPosition);
    }

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        base.Harm(dmg, knockBack, knockUp, source);

        GetComponentInChildren<IsAggro>().StartAggro();
    }

    public override void Die()
    {
        base.Die();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Entity>().Harm(1, knockback, knockback, gameObject);

            Harm(0,knockback/3,knockback/3,other.gameObject);

        }
    }
}
