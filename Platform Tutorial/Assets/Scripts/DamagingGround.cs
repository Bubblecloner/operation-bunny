using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingGround : MonoBehaviour
{

    public float time;


    private float damageOverTime;
    private Health healthScript;



    void Start()
    {
        healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //InvokeRepeating("DealDamage", 0.125f, time);
            healthScript.InvokeRepeating("TakeDamage", 0.125f, time);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            healthScript.CancelInvoke();
        }
    }


    //private void DealDamage()
    //{
    //    healthScript.health--;
    //}

}

