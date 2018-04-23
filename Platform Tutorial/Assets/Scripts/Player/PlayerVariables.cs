using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour {

    public float bounceHeight = 3f;
    public float health = 100;
    public Transform startPosition;
    public GameObject coinParticles;
    public AudioClip coinPickup;

    private float damageTimer;
    private AudioSource myAudioSource;
    private Rigidbody2D rgbd2d;

	void Start ()
    {
        health = 100;
        myAudioSource = GetComponent<AudioSource>();
        rgbd2d = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        if(damageTimer > 0)
            damageTimer -= Time.deltaTime;

        GameController.gameControllerInstance.playerHealth = health;
    }

    public void Harm(float dmg)
    {
        if(damageTimer <= 0f)
        {
            health -= dmg;
            damageTimer = 1;
            GameController.gameControllerInstance.ScreenShake();
        }

        if(health < 1)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = startPosition.position;
        health = 100;
    }

    //Call this when the player should bounce of something
    public void Bounce()
    {
        rgbd2d.velocity = new Vector2(rgbd2d.velocity.x, bounceHeight);
    }

    //This runs when the player picks up a coin
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            Instantiate(coinParticles, other.transform.position, Quaternion.identity);
            GameController.gameControllerInstance.coins++;
            myAudioSource.pitch = Random.Range(0.5f, 1.5f);
            myAudioSource.PlayOneShot(coinPickup, 0.5f);
        }
    }
}
