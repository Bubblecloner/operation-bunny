using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

	void Start () {
		
	}

    private void OnEnable()
    {
        //pause Game
    }

    private void OnDisable()
    {
        //unpause game
    }

    void Update () {
		
	}

    public void ExitLevel()
    {
        //exit level
        GameController.gameControllerInstance.LoadLevel(6);
    }
}
