using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController gameControllerInstance;
    public Text coinText;
    public Text arrowCount;
    public GameObject heartIcon;
    public GameObject heartParent;
    public GameObject[] potionIcons;
    public GameObject potionParent;
    public GameObject player;
    public GameObject potionSwapIcon;

    public float playerHealth;

    [HideInInspector]
    public int coins;
    [HideInInspector]
    public int[] potions;
    [HideInInspector]
    public int arrows;

    private int consecutive = 10;
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
        arrowCount.text = arrows.ToString();
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
            GameObject temp = Instantiate(potionIcons[0], potionParent.transform, false);
            temp.transform.localPosition = new Vector3(80 * (potionParent.transform.childCount - 1) + 95, 80, 0);
            temp.name = "Potion" + i.ToString();
            if (i != 0)
                temp.transform.localScale = new Vector2(temp.transform.localScale.x * 0.7f, temp.transform.localScale.y * 0.7f);
            else
                //moves first potion for visual reasons
                temp.transform.localPosition = new Vector2(80, 80);
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

    public void SwapPotions()
    {
        player.GetComponent<PotionHandler>().SwapPotions();
        if (potions[0] != 0)
        {
            GameObject temp = Instantiate(potionSwapIcon, player.transform);
            temp.GetComponent<SpriteRenderer>().sprite = potionIcons[potions[0]].GetComponent<Image>().sprite;
            temp.transform.localPosition = new Vector2(0, 1.2f);
            temp.transform.localScale = new Vector2(0.2f, 0.2f);
            temp.GetComponent<SpriteRenderer>().sortingOrder = consecutive;
            consecutive++;
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
        CarryOverInfo.carryOverInfoInstance.UpdatePlayerStats(GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerVariables>().gameObject);

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
