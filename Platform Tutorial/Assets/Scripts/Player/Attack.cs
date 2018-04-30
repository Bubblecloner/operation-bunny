using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public float knockback;
    public string targetTag = "Enemy";

    private int damage = 1;

	void Start ()
    {
        damage = GetComponentInParent<Entity>().Damage;
    }
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == targetTag)
        {
            other.GetComponent<Entity>().Harm(1, knockback, knockback/2, transform.parent.transform.position.x > transform.position.x);
        }
    }
}
