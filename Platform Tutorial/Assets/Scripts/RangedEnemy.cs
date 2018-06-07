using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour {

    public GameObject projectile;
    public float health;
    public Vector2 velocity;
    private Vector2 offset = new Vector2(0.0f, 0.0f);
    private float shootingCD = 0.5f;
    private bool canShoot = true;
    public float chaseRange;
    public Transform target;
    public float speed = 2.5f;


    private bool foundPlayer = false;


    void Start ()
    {
		
	}
	
	
	void Update ()
    {

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < chaseRange)
        {
            foundPlayer = true;
            
        }

        if (foundPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }


        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
