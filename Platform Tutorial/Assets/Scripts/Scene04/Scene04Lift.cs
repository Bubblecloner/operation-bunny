using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scene04Lift : MonoBehaviour {

    private Rigidbody2D rgbd;
    private bool moving;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        moving = false;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rgbd.MovePosition(transform.position + new Vector3(3.0f, 0, 0) * Time.fixedDeltaTime);
        }
    }

    public void Drop()
    {
        rgbd.DOMove(new Vector2(transform.position.x, -4), 1.0f);
        Invoke("Move", 2.0f);
    }

    private void Move()
    {
        moving = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }

        if (other.collider.CompareTag("Stop"))
        {
            moving = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
