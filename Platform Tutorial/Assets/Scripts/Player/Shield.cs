using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Entity {

	protected override void Start () {
        base.Start();

	}
	
	void Update () {
		
	}

    public override void Harm(int dmg, float knockBack, float knockUp, GameObject source)
    {
        health--;

        source.GetComponentInParent<Entity>().Harm(0,6,3,gameObject);
    }
}
