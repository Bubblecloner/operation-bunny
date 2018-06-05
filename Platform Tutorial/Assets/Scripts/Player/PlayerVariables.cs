using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : Entity {


    public bool bossFight;
    public GameObject coinParticles;
    public AudioClip coinPickup;
    public AudioClip hurt;
    public AudioClip fallDeath;
    public GameObject bonusDamageParticles;
    public GameObject invincibilityParticles;

    public int[] healthUpgrades;
    public int[] damageUpgrades;

    private AudioSource myAudioSource;
    private CarryOverInfo COInfo;

    protected override void Start ()
    {
        base.Start();
        COInfo = CarryOverInfo.carryOverInfoInstance;
        maxHealth = healthUpgrades[COInfo.upgrades[0]];
        damage = damageUpgrades[COInfo.upgrades[1]];


        health = maxHealth;
        myAudioSource = GetComponent<AudioSource>();
    }
	
	protected override void Update ()
    {
        base.Update();

        GameController.gameControllerInstance.playerHealth = health;
    }

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        if (!(GetComponent<PlatformInputs>().Shielding && source.GetComponent<Collider2D>().IsTouching(GetComponentInChildren<Shield>().GetComponent<Collider2D>())))
        {
            if (damageTimer <= 0f)
            {
                health -= dmg;
                damageTimer = 1;
                GameController.gameControllerInstance.ScreenShake();

                myAudioSource.pitch = Random.Range(0.5f, 1.5f);
                myAudioSource.PlayOneShot(hurt, 0.5f);
            }

            Knockback(knockBack, knockUp, source.transform.position.x > transform.position.x);

            if (health < 1)
            {
                Die();
            }
        }
    }

    public void Invincible(float time)
    {
        damageTimer = time;


        GameObject temp = Instantiate(invincibilityParticles, transform);
        temp.transform.localPosition = new Vector2(0, 0.5f);
        temp.GetComponent<DestroyAfterLifetime>().lifetime = time;
    }

    public void TempDamageIncrease(int bonusDamage,float time)
    {
        damage += bonusDamage;

        for(int i=0; i < bonusDamage; i++)
        Invoke("DamageDecrease", time);


        GameObject temp = Instantiate(bonusDamageParticles,transform);
        temp.transform.localPosition = new Vector2(0, 0.5f);
        temp.GetComponent<DestroyAfterLifetime>().lifetime = time;
    }

    private void DamageDecrease()
    {
        damage--;
    }

    public override void Die()
    {
        Invoke("Respawn", 2);

        base.Die();
    }

    public void Respawn()
    {
        if (!bossFight)
            GameController.gameControllerInstance.DeathGame();
        else
            GameController.gameControllerInstance.ReloadLevel();
    }

    public void FallDeath()
    {
        health = 0;
        GameController.gameControllerInstance.ScreenShake();

        myAudioSource.pitch = Random.Range(0.5f, 1.5f);
        myAudioSource.PlayOneShot(fallDeath, 0.5f);

        Die();
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
            //Instantiate(coinParticles, other.transform.position, Quaternion.identity);
            GameController.gameControllerInstance.coins++;
            myAudioSource.PlayOneShot(coinPickup, 0.5f);
        }
    }
}
