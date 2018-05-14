using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public LayerMask layerMask;
    public int penetration = 1;
    public float pickChance = 0.5f;

    private bool flying = false;
    private bool pickable = false;
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
        if(other.tag == "Enemy" && flying)
        {
            other.GetComponent<Entity>().Harm(1,0,0,gameObject,0.1f);
            penetration--;
        }

        if ((!other.isTrigger || ( other.tag == "Enemy" && penetration <= 0)) && other.tag != "Player" && flying)
        {

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.parent = other.transform;


            flying = false;
            if ((Vector2)transform.localPosition != Vector2.zero)
                hitPoint = transform.localPosition;
            else
                hitPoint = Vector2.one;


            GetComponent<Rigidbody2D>().gravityScale = 0;

            if (other.tag != "Enemy")
                pickable = true;
        }

        if(pickable && other.tag == "Player")
        {
            if(Random.Range(0f,1f) < pickChance)
            {
                other.GetComponentInParent<PlatformInputs>().AddArrows(1);
            }
            Destroy(gameObject);
        }
    }
}
