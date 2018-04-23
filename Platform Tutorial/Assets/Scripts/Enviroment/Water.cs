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
            float playerHeight = other.transform.position.y - transform.position.y;
            float depth = height - playerHeight;

            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,buoyancy*Mathf.Abs(Mathf.Pow(depth,4))/2));

            //DO NOT ENTER
            /*if (other.GetComponent<Rigidbody2D>().velocity.y >  depth / height * 2 && depth / height > 1 / 2)
                other.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(other.GetComponent<Rigidbody2D>().velocity, new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, depth / height * 2),1000);
                */
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().velocity /= 2;
        }
    }


}
