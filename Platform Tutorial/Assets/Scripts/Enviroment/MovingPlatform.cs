using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform[] targets;
    public float moveSpeed = 3.0f;
    
    private int currentTarget;

	void Start ()
    {
        currentTarget = 0;
	}
	
	void FixedUpdate ()
    {
        transform.position = Vector2.MoveTowards(transform.position, targets[currentTarget].position, moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, targets[currentTarget].position) < 0.1)
        {
            currentTarget++;
            if (currentTarget > targets.Length - 1)
                currentTarget = 0;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.transform.parent = null;
    }
}
