using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController gameControllerInstance;
    public Text coinText;
    public Slider healthSlider;

    public float playerHealth;

    [HideInInspector]
    public int coins;

    private Quaternion originalCameraRotation;

	void Start ()
    {
        gameControllerInstance = this;
        coins = 0;
        originalCameraRotation = Camera.main.transform.rotation;
	}
	
	void Update ()
    {
        coinText.text = coins.ToString();
        healthSlider.value = playerHealth;

        healthSlider.GetComponentInChildren<Image>().color = new Color32((byte)(int)(120 * playerHealth / 100), 0, 0, 255);
        healthSlider.GetComponentsInChildren<RectTransform>()[2].GetComponentInChildren<Image>().color = new Color32((byte)(int)(255 - 255 * Mathf.Pow(playerHealth / 100,2)), (byte)(int)(255 * playerHealth / 100), 0, 255);
    }

    public void ScreenShake()
    {
        Camera.main.DOShakeRotation(0.2f, 6, 40, 90);

        Invoke("ResetCameraRotation", 0.2f);
    }

    private void ResetCameraRotation()
    {
        Camera.main.transform.rotation = originalCameraRotation;
    }

    public void LoadLevel(int levelToLoad)
    {
        int previousCoins = PlayerPrefs.GetInt("coins");
        previousCoins += coins;
        PlayerPrefs.SetInt("coins", previousCoins);
        SceneManager.LoadScene(levelToLoad);
    }
}
