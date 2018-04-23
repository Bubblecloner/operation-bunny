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
    public GameObject heartParent;

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
        //coinText.text = coins.ToString();


        if (playerHealth > heartParent.transform.childCount)
            RegenHeart();
        else if (playerHealth < heartParent.transform.childCount)
            DestroyHeart();

        

    }

    private void RegenHeart()
    {
        Instantiate(heartIcon,heartParent.transform,false).transform.localPosition = new Vector3(100*(heartParent.transform.childCount-1),0,0);
    }

    private void DestroyHeart()
    {
        Destroy(heartParent.transform.GetChild(heartParent.transform.childCount - 1));
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
