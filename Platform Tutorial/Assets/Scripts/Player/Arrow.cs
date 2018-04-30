using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public LayerMask layerMask;
    public int penetration = 1;

    private bool flying = false;
    private Vector2 hitPoint = Vector2.zero;
    
	
	void Update ()
    {
        if (flying)
        {
            transform.rotation = Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.up, GetComponent<Rigidbody2D>().velocity), new Vector3(0, 0, 1));
        }
        else if (hitPoint != Vector2.zero)
            transform.localPosition = hitPoint;
	}

    public void Shot()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<BoxCollider2D>().enabled = true;
        flying = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Entity>().Harm(1,0,0,gameObject);
            penetration--;
        }

        if ((!other.isTrigger || ( other.tag == "Enemy" && penetration <= 0)) && other.tag != "Player")
        {

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.parent = other.transform;


            flying = false;
            hitPoint = transform.localPosition;


            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
