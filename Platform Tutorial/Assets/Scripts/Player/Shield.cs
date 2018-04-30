using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity {
    
	protected override void Start () {
        base.Start();

        gameObject.SetActive(false);

        GetComponentInParent<PlatformInputs>().ShieldActive = true;
    }
	
	void Update () {
		
	}

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        health--;

        source.GetComponentInParent<Entity>().Harm(0,6,3,gameObject);

        if (health <= 0)
            Die();
    }

    public override void Die()
    {
        GetComponentInParent<PlayerVariables>().Harm(0,6,3,gameObject);
        GetComponentInParent<PlatformInputs>().ShieldActive = false;
    }

    public void Activate()
    {
        health = maxHealth;
        GetComponentInParent<PlatformInputs>().ShieldActive = true;
    }
}
