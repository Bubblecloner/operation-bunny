using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour {

    private float startSpeeed;
    private bool active = true;

    public float speedBoost = 1.5f;
    public GameObject speedEffect;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (active && other.CompareTag("Player"))
        {
            active = false;
            startSpeeed = other.GetComponent<PlatformInputs>().speed;
            other.GetComponent<PlatformInputs>().speed *= speedBoost;
            speedEffect.GetComponent<DestroyAfterLifetime>().lifetime = 3f;
            Instantiate(speedEffect, new Vector3(other.transform.position.x, other.transform.position.y + 0.43f, other.transform.position.z), Quaternion.identity, other.transform);
            yield return new WaitForSeconds(3f);
            other.GetComponent<PlatformInputs>().speed = startSpeeed;
            active = true;
        }
    }

}
