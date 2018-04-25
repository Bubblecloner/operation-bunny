using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTest : MonoBehaviour {

    public bool test = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(test)
        {
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0,10),ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(new Vector2(0, 100), Vector2.zero, 0);
            test = false;
        }

	}
}
