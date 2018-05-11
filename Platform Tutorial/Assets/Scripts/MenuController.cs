using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public int scene = 1;

    public Text coinText;

    public void Start()
    {
        coinText.text = "Coins: " + PlayerPrefs.GetInt("coins");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
