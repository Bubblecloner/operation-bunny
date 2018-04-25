using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : Entity {

    
    public Transform startPosition;
    public GameObject coinParticles;
    public AudioClip coinPickup;
    public AudioClip hurt;

    private float damageTimer;
    private AudioSource myAudioSource;

	void Start ()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        health = 3;
        myAudioSource = GetComponent<AudioSource>();
	}
	
	void Update ()
    {

        if(damageTimer > 0)
            damageTimer -= Time.deltaTime;

        GameController.gameControllerInstance.playerHealth = health;
    }

    public override void Harm(int dmg, float knockBack, float knockUp, bool cameFromRight)
    {
        if (damageTimer <= 0f)
        {
            health -= dmg;
            damageTimer = 1;
            GameController.gameControllerInstance.ScreenShake();

            myAudioSource.pitch = Random.Range(0.5f, 1.5f);
            myAudioSource.PlayOneShot(hurt, 0.5f);
        }

        Knockback(knockBack, knockUp, cameFromRight);

        if (health < 1)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = startPosition.position;
        health = 3;
    }

    //Call this when the player should bounce of something
    public void Bounce(float bounceHeight)
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
