using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public Vector2 velocity;
    private Vector2 offset = new Vector2(0.0f, 0.0f);
    private float shootingCD = 0.4f;
    private bool canShoot = true;

    void Start()
    {
     
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(1) && canShoot)
        {

            GameObject go = (GameObject)Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);

            go.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);

            StartCoroutine(CanShoot());
        }
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingCD);
        canShoot = true;
    }
}