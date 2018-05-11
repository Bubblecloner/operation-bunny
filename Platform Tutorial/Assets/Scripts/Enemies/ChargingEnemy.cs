using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : WalkingEnemy {

    private float normalSpeed;

    protected override void Start()
    {
        base.Start();
        facingRight = false;
        normalSpeed = speed;
    }

    protected override void Update ()
    {
        base.Update();
    }

    protected override void FaceEnemy(Vector2 target)
    {
        if (health > 0)
        {
            if (Vector2.Distance(transform.position, target) < triggerRange && attackTimer < 0)
            {
                speed = 0;
            }
        }


        base.FaceEnemy(target);


    }

    protected override void Attack()
    {
        GameObject temp = Instantiate(attack, transform, false);
        temp.transform.localScale = attackSize;

        if (facingRight)
            temp.transform.localPosition = new Vector2(1, 0f);
        else
            temp.transform.localPosition = new Vector2(-1, 0f);

        temp.GetComponent<Attack>().targetTag = "Player";

        if (facingRight)
            rgbd2d.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
        else
            rgbd2d.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);

        stunTimer = hitStun;

        speed = normalSpeed;
    }

    protected override void Flip()
    {
        base.Flip();

        rgbd2d.velocity = new Vector2(0, rgbd2d.velocity.y);
    }
}
