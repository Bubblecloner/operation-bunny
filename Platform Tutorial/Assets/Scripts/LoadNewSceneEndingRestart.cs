using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneEndingRestart : MonoBehaviour {

    

    public void LoadNextScene()
    {
        // Ändra scennummer här!
        SceneManager.LoadScene(2);
    }
}
