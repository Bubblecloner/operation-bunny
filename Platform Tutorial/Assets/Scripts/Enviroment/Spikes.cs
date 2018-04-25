﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public float force = 1;
    public float forceUp = 1;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerVariables>().Harm(damage,force,forceUp,other.transform.position.x > 0);
        }
    }




    void Start ()
    {
		
	}


    void Update ()
    {
		
	}
}
