using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smooth = 5.0f;
    public float offset = 1.0f;
    public float maxY = 2;
	
	void FixedUpdate ()
    {
        if (target.GetComponent<PlayerVariables>().health > 0)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + offset, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, targetPosition.y, transform.position.z), Time.deltaTime * smooth);
        }
	}
}
