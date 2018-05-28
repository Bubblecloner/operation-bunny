using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTriggerScene04 : MonoBehaviour {

    public GameObject lift;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lift.GetComponent<Scene04Lift>().Drop();
        }
    }
}
