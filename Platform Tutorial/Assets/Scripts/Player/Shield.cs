using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity {

    public AudioClip shieldHitSound;

	protected override void Start () {
        base.Start();

        gameObject.SetActive(false);

        GetComponentInParent<PlatformInputs>().ShieldActive = true;
    }
	
	protected override void Update () {

	}

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        health--;

        source.GetComponentInParent<Entity>().Harm(0,6,3,gameObject);

        if (health <= 0)
            Die();


        GetComponentInParent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        GetComponentInParent<AudioSource>().PlayOneShot(shieldHitSound, 0.5f);
    }

    public override void Die()
    {
        GetComponentInParent<PlayerVariables>().Harm(0,6,3,gameObject);
        GetComponentInParent<PlatformInputs>().ShieldActive = false;
        GetComponentInParent<PlatformInputs>().StartShieldTimer();
    }

    public void Activate()
    {
        health = maxHealth;
        GetComponentInParent<PlatformInputs>().ShieldActive = true;
    }
}
