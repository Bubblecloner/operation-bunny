﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    public GameObject CarryOver;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetButtonDown("Cheat"))
        {
            int[] temp = { 1, 2, 3 };
            GetComponent<PotionHandler>().Potions = temp;
        }

        if (Input.GetButtonDown("Cheat2"))
        {
            if (!GameObject.Find("Carry Over Info"))
                Instantiate(CarryOver);
        }
    }
}
