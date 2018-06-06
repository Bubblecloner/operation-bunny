using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

	void Start () {
		
	}


    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            UnPauseGame();
        }
    }


    private void OnEnable()
    {
        //pause Game
        /*GameObject[] entities = SceneManager.GetActiveScene().GetRootGameObjects();
        
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].GetComponent<Entity>())
                entities[i].GetComponent<Entity>().paused = true;
        }*/
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        //unpause game
        /*GameObject[] entities = SceneManager.GetActiveScene().GetRootGameObjects();
        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].GetComponent<Entity>())
                entities[i].GetComponent<Entity>().paused = false;
        }*/
        Time.timeScale = 1;
    }

    public void ExitLevel()
    {
        //exit level
        GameController.gameControllerInstance.LoadLevel(5);
    }

    public void UnPauseGame()
    {
        //unpause game
        gameObject.SetActive(false);
    }
}
