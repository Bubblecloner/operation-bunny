using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

    public float speed = 3;
    public int damage = 2;

    private bool hit = false;
    
	
	void Update ()
    {

        if (transform.localScale.x > 0)
            transform.position += (Vector3)Vector2.right * speed * Time.deltaTime;
        else
            transform.position += (Vector3)Vector2.left * speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !hit)
        {
            other.GetComponentInParent<PlayerVariables>().Harm(damage, 0, 0, gameObject);
            hit = true;
        }
    }
}
