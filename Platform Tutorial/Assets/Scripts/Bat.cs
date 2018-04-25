﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Entity {

    public float speed = 3;
    public float turning = 1;
    public float knockback = 80;

    private bool aggro;
    private Vector2 startPosition;

	void Start ()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
	}
	
	void Update ()
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
        
        Vector2 vector =   new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized * speed;
        
        transform.Translate(vector);

        //Debug.Log(Vector2.MoveTowards(transform.position, target, speed) + "," + new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized * speed);
    }

    private void Idle()
    {
        if(Vector2.Distance(transform.position,startPosition) > 1)
            MoveTowardTarget(startPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerVariables>().Harm(1, knockback, knockback, other.transform.position.x < transform.position.x);

            Knockback(knockback, knockback, other.transform.position.x > transform.position.x);
        }
    }
}