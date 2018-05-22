using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPS : MonoBehaviour {

    public Button yesButton;
    public Button noButton;

    int randomS;
    int randomM;
    int randomE;

    private void Awake()
    {
        randomS = Random.Random.Range(1, 4);
        randomM = Random.Random.Range(1, 4);
        randomE = Random.Random.Range(1, 4);
    }

	void Update ()
    {
        if (randomS == 1)
            yesButton;
	}
}
