using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

private bool triggered;
private SpriteRenderer rend;

public class blackfadein : MonoBehaviour {

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
