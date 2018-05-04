using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyboxrotate : MonoBehaviour {

    private Skybox sky;

	void Start ()
    {
        sky = GetComponent<Skybox>();
	}
	
	void Update ()
    {
        sky.material.SetFloat("_Rotation", Time.time);
	}
}
