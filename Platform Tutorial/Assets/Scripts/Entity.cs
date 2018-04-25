using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public int health = 3;
    protected Rigidbody2D rgbd2d;

    void Start ()
    {

        rgbd2d = GetComponent<Rigidbody2D>();
    }
	
	void Update () {

    }

    public virtual void Harm(int dmg, float knockBack, float knockUp, bool cameFromRight)
    {
            health -= dmg;

        Knockback(knockBack, knockUp, cameFromRight);

        if (health < 1)
        {
            Die();
        }
    }

    public void Knockback(float force, float forceUp, bool cameFromRight)
    {
        if (cameFromRight)
            rgbd2d.AddForce(new Vector2(-force, forceUp),ForceMode2D.Impulse);
        else
            rgbd2d.AddForce(new Vector2(force, forceUp),ForceMode2D.Impulse);

        Invoke("CancelKnockback",1);

    }

    private void CancelKnockback()
    {
        Debug.Log(rgbd2d.velocity);
        rgbd2d.velocity = new Vector2(0,rgbd2d.velocity.y);
    }

    private void Die()
    {
        //add later
    }
}
