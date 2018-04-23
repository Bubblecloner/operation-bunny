using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public float force = 1;
    public float forceUp = 1;
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerVariables>().Harm(damage);
            other.GetComponent<PlayerVariables>().Knockback(force, forceUp);
        }
    }




    void Start ()
    {
		
	}


    void Update ()
    {
		
	}
}
