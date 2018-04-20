using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetComponent<Rigidbody2D>().velocity.y < -0.01)
        {
            GetComponentInParent<Slime>().Die();
            gameObject.SetActive(false);
            other.GetComponent<PlayerVariables>().Bounce();
        }
    }
}
