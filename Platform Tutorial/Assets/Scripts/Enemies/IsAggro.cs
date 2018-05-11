using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAggro : MonoBehaviour {

    public float outOfAggroTime;
    public float failedResetTime = 0.5f;

    public bool Aggro { get; private set; }

    private float aggroTimer;
    private float failedTimer = -1;

	void Start ()
    {
        aggroTimer = -1;
	}
	
	void Update ()
    {
        if (aggroTimer > 0)
            aggroTimer -= Time.deltaTime;
        else if (aggroTimer <= 0 && aggroTimer > -1)
        {
            Aggro = false;
            aggroTimer = -1;
        }

        failedTimer -= Time.deltaTime;
        
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Aggro == false && failedTimer <= 0)
        {
            Aggro = true;
            aggroTimer = -1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            aggroTimer = outOfAggroTime;
        }
    }

    public void FailedToReach()
    {
        Aggro = false;
        failedTimer = failedResetTime;
    }
}
