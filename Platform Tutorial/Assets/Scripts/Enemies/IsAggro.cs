using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAggro : MonoBehaviour {

    public float outOfAggroTime;

    public bool Aggro { get; private set; }
    private float aggroTimer;

	void Start ()
    {
        aggroTimer = -1;
	}
	
	void Update ()
    {
        if (aggroTimer > 0)
            aggroTimer -= Time.deltaTime;
        else if (aggroTimer <= 0 && aggroTimer > -1)
        {
            Aggro = false;
            aggroTimer = -1;
        }
        
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Aggro = true;
            aggroTimer = -1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            aggroTimer = outOfAggroTime;
        }
    }
}
