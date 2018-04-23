using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController gameControllerInstance;
    public Text coinText;
    public GameObject heartIcon;
    public Transform heartRefrence;

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

        if (heartRefrence.childCount  < playerHealth)
            Instantiate(heartIcon, new Vector3(50* (heartRefrence.childCount) + 20, 520, 0), Quaternion.identity, heartRefrence);
        if (heartRefrence.childCount  > playerHealth)
            DestroyHeart();

    }

    private void DestroyHeart()
    {
        Destroy(heartRefrence.GetChild(heartRefrence.childCount - 1).gameObject);
        Debug.Log("test");
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
