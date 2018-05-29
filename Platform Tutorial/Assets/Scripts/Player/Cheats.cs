using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour {

    public GameObject CarryOver;

	void Start () {

        if (!GameObject.Find("Carry Over Info(Clone)"))
        {

            Instantiate(CarryOver);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (CarryOverInfo.carryOverInfoInstance.unlockedLevels.Count == 1)
        {
            CarryOverInfo.carryOverInfoInstance.nextUnlockingLevels = new string[] { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Boss 1", "Boss 2", "Boss 3", "Bunny", "Bonus 1", "Bonus 2", "Bonus 3" };
            CarryOverInfo.carryOverInfoInstance.UnlockLevels();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Debug.Log("Cheats Enabled");
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetButtonDown("Cheat"))
        {
            int[] temp = { 1, 2, 3 };
            GetComponent<PotionHandler>().Potions = temp;
        }

        if (Input.GetButtonDown("Cheat2"))
        {
            if (!GameObject.Find("Carry Over Info"))
                Instantiate(CarryOver);
        }

        if (Input.GetButtonDown("Cheat3"))
        {
            CarryOverInfo.carryOverInfoInstance.Load();
            GameController.gameControllerInstance.LoadLevel(5);
        }

        if (Input.GetButtonDown("Cheat4"))
        {
            GameController.gameControllerInstance.LoadLevel(5);
        }

        if (Input.GetButtonDown("Cheat5"))
        {

        }
    }
}
