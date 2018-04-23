using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public float buoyancy;
    private float height;

	void Start ()
    {
        height = transform.localScale.y * transform.GetComponent<BoxCollider2D>().size.y;
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            float distance = other.GetComponent<BoxCollider2D>().Distance(surface.GetComponent<BoxCollider2D>()).distance;

            if (distance < 0)
                distance = 0;

            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,buoyancy*distance));
            Debug.Log(distance);
        }
    }

    
}
