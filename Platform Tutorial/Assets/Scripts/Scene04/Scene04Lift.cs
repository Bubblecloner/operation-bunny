using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Scene04Lift : MonoBehaviour {

    public Image arrow;

    private Rigidbody2D rgbd;
    private bool moving;
    private Transform playerPosition;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        moving = false;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        arrow.enabled = false;
        FadeArrowOut();
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rgbd.MovePosition(transform.position + new Vector3(3.0f, 0, 0) * Time.fixedDeltaTime);
        }

        arrow.transform.position = new Vector3(transform.position.x, playerPosition.position.y + 5, transform.position.z);
    }

    public void Drop()
    {
        rgbd.DOMove(new Vector2(transform.position.x, -4), 1.0f);
        Invoke("Move", 2.0f);
        arrow.enabled = true;
    }

    private void Move()
    {
        moving = true;
    }

    private void FadeArrowIn()
    {
        arrow.DOFade(0.8f, 1.0f);
        Invoke("FadeArrowOut", 1.0f);
    }

    private void FadeArrowOut()
    {
        arrow.DOFade(0.4f, 1.0f);
        Invoke("FadeArrowIn", 1.0f);
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
