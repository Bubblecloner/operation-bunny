using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : WalkingEnemy {

    protected override void Start()
    {
        base.Start();
        facingRight = false;
    }

    protected override void Update ()
    {
        base.Update();
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
    }
}
