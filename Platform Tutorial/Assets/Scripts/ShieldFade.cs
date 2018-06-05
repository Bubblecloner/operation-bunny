using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFade : MonoBehaviour {

    public float blinkRate;
    public float blinkMax;
    public float blinkMin;

    private float blinkTimer;
    private bool increasing;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<SpriteRenderer>().color = new Color(1,1,1,blinkTimer);


        if (increasing)
        {
            blinkTimer += Time.deltaTime/blinkRate;

            if (blinkTimer > blinkMax)
                increasing = false;
        }
        else
        {
            blinkTimer -= Time.deltaTime/blinkRate;

            if (blinkTimer < blinkMin)
                increasing = true;
        }
	}
}
