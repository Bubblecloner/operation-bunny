using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class blackfadein : MonoBehaviour {

	void Start () {
		
	}

   

	void Update ()
    {
        GetComponent<"black">();
        black.DoFade(0, 2.0f);
	}
}
