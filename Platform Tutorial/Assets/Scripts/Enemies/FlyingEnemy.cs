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

    void Update()
    {
        aggro = GetComponentInChildren<IsAggro>().Aggro;

        if (aggro)
        {
            MoveTowardTarget(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
        else
            Idle();

    }

    private void MoveTowardTarget(Vector2 target)
    {
        if (health > 0)
        {


            Vector2 vector = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized * speed;

            transform.Translate(vector);
        }
    }

    private void Idle()
    {
        if (Vector2.Distance(transform.position, startPosition) > 1)
            MoveTowardTarget(startPosition);
    }

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        knockback /= 2;
        knockUp /= 3;
        base.Harm(dmg, knockBack, knockUp, source);
    }

    public override void Die()
    {
        base.Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Entity>().Harm(1, knockback, knockback, gameObject);

            Knockback(knockback, knockback, other.transform.position.x > transform.position.x);
        }
    }
}
