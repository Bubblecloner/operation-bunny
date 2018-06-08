using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text gamestatus;
    private int numberOfEnemiesAlive;
    private int lastArraySize;
    public GameObject goPanel;


    void Start()
    {
        numberOfEnemiesAlive = 0;
        goPanel.SetActive(false);
    }

    void Update()
    {

        EnemiesAlive();
    }


    void EnemiesAlive()
    {


        numberOfEnemiesAlive = (GameObject.FindGameObjectsWithTag("Enemy")).Length;

        if(numberOfEnemiesAlive <= 0)
        {
            gamestatus.text = gamestatus.text.Substring(0, 19);
            gamestatus.text = gamestatus.text + numberOfEnemiesAlive;
        }

        if (numberOfEnemiesAlive != lastArraySize)
        {
            gamestatus.text = gamestatus.text.Substring(0, 19);
            gamestatus.text = gamestatus.text + numberOfEnemiesAlive;

        }

        if (GameObject.FindGameObjectWithTag("Door").activeInHierarchy == true)
        {
            gamestatus.text = "The Door Has Spawned";
        }
    }


    public void GameOverTryAgain()
    {
        goPanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;

    }

    public void ExitTopDown()
    {
        SceneManager.LoadScene(5);
    }
}
