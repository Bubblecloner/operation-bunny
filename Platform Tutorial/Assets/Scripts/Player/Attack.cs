﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public float knockback;
    public string targetTag = "Enemy";

    private int damage = 1;

	void Start ()
    {
        if (transform.parent.transform.position.x > transform.position.x)
            transform.localScale *= new Vector2(-1, 1);
        damage = GetComponentInParent<Entity>().Damage;
        GetComponentInParent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        GetComponentInParent<PlayerVariables>().PlayAttackSound();
    }
	
	void Update ()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == targetTag)
        {
            other.GetComponent<Entity>().Harm(damage, knockback, knockback/2, transform.parent.gameObject);
        }
    }
}
