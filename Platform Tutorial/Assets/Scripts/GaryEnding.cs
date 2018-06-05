using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GaryEnding : MonoBehaviour {

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Movement(Vector3 destination, float timer)
    {
        transform.DOMove(destination, timer);
        anim.SetBool("Moving", true);
        Invoke("StopMovement", timer);
    }

    private void StopMovement()
    {
        anim.SetBool("Moving", false);
    }
}
