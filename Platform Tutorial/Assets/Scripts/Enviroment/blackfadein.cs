using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class blackfadein : MonoBehaviour {

    private bool triggered;
    private SpriteRenderer rend;

    void Start ()
    {
        triggered = false;
        rend = GetComponent<SpriteRenderer>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player") && !triggered)
        {
            triggered = true;
            rend.DOFade(0, 2.0F);
        }
    }

    void Update ()
    {
	}
}
