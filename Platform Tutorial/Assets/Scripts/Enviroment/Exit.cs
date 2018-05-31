using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

    public int levelToLoad;
    public float delay = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Invoke("LoadLevel",delay);
        }
    }


    private void LoadLevel()
    {

        GameController.gameControllerInstance.ClearLevel();
    }
}
