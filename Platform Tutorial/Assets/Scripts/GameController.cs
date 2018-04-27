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
    public GameObject[] potionIcons;
    public GameObject potionParent;

    public float playerHealth;

    [HideInInspector]
    public int coins;
    [HideInInspector]
    public int[] potions;

    private Quaternion originalCameraRotation;

	void Start ()
    {
        gameControllerInstance = this;
        coins = 0;
        originalCameraRotation = Camera.main.transform.rotation;

        InstantiatePotions();
	}
	
	void Update ()
    {
        //coinText.text = coins.ToString();
        DisplayPotions();


        if (playerHealth > heartParent.transform.childCount)
            RegenHeart();
        else if (playerHealth < heartParent.transform.childCount)
            DestroyHeart();

        

    }

    private void InstantiatePotions()
    {
        for(int i = potionParent.transform.childCount; i < potions.Length; i++)
        {
            GameObject temp = Instantiate(potionIcons[potions[i]], potionParent.transform, false);
            temp.transform.localPosition = new Vector3(130 * (potionParent.transform.childCount - 1) + 80, 80, 0);
            temp.name = "Potion" + i.ToString();
        }
    }

    private void DisplayPotions()
    {
        for(int i = 0; i < potions.Length; i++)
        {
            if (potionParent.transform.childCount == potions.Length)
                potionParent.transform.Find("Potion" + i.ToString()).GetComponent<Image>().sprite = potionIcons[potions[i]].GetComponent<Image>().sprite;
            else
                InstantiatePotions();
        }
    }

    private void RegenHeart()
    {
        Instantiate(heartIcon,heartParent.transform,false).transform.localPosition = new Vector3(100*(heartParent.transform.childCount-1),0,0);
    }

    private void DestroyHeart()
    {
        if(heartParent.transform.childCount > 0)
        Destroy(heartParent.transform.GetChild(heartParent.transform.childCount - 1).gameObject);
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

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
