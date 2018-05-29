using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    
    protected int health = 3;
    public int maxHealth = 3;
    public float hitStun = 2;
    public bool paused = false;
    protected Rigidbody2D rgbd2d;
    protected int damage = 1;
    protected float stunTimer;
    protected float damageTimer;

    protected virtual void Start ()
    {
        health = maxHealth;
        rgbd2d = GetComponent<Rigidbody2D>();
    }
	
	protected virtual void Update ()
    {
        if (!paused)
        {
            rgbd2d.isKinematic = false;
            stunTimer -= Time.deltaTime;
            damageTimer -= Time.deltaTime;

            if (damageTimer > 0 && Time.frameCount % 8 <= 3)
            {
                Color temp = GetComponentInChildren<SpriteRenderer>().color;
                GetComponentInChildren<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 0.5f);
            }
            else
            {
                Color temp = GetComponentInChildren<SpriteRenderer>().color;
                GetComponentInChildren<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, 1);
            }
        }
        else
            rgbd2d.isKinematic = true;
    }

    public virtual void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        stunTimer = hitStun;

        if (damageTimer < 0)
        {

            health -= dmg;

            Knockback(knockBack, knockUp, source.transform.position.x > transform.position.x);

            if (health < 1)
            {
                Die();
            }

            damageTimer = 1;
        }
    }

    public virtual void Harm(int dmg, float knockBack, float knockUp, GameObject source, float stunTime)
    {
        Harm(dmg, knockBack, knockUp, source);
        stunTimer = stunTime;
    }

    public void Knockback(float force, float forceUp, bool cameFromRight)
    {
        if (cameFromRight)
            rgbd2d.AddForce(new Vector2(-force, forceUp),ForceMode2D.Impulse);
        else
            rgbd2d.AddForce(new Vector2(force, forceUp),ForceMode2D.Impulse);

    }

    public virtual void Die()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var item in colliders)
        {
            item.enabled = false;
        }

        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 13f), ForceMode2D.Impulse);
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        rgbd2d.gravityScale = 3;

        Invoke("DisableObject", 2.0f);
    }

    public virtual void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
    }

    private void DisableObject()
    {
        Destroy(gameObject);
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
    }
}
