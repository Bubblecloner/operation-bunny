using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup : MonoBehaviour {

    public int potionId;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponentInParent<PotionHandler>().AddPotion(potionId))
            {
                Destroy(gameObject);
            }
        }
    }
}
