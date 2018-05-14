using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : FlyingEnemy {
    
    private Animator anim;

	protected override void Start ()
    {
        base.Start();
        anim = GetComponent<Animator>();
	}
	
	protected override void Update ()
    {
        base.Update();

    }

    protected override void MoveTowardTarget(Vector2 target)
    {
        if (health > 0)
        {


            Vector2 vector = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized * speed;

            transform.Translate(vector);


            if (vector.x < 0)
                anim.SetFloat("speed", -1);
            else
                anim.SetFloat("speed", 1);

        }
    }

    protected override void Idle()
    {
        if (Vector2.Distance(transform.position, startPosition) > 1)
            MoveTowardTarget(startPosition);
        else
            anim.SetFloat("speed", 0);
    }

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        base.Harm(dmg, knockBack, knockUp, source);
    }

    public override void Die()
    {
        base.Die();
        
        anim.SetBool("death", true);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
