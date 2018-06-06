using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldCheats : MonoBehaviour {

    public GameObject CarryOver;
    public OverworldController oWController;

	void Start ()
    {


        Debug.Log("Cheats Enabled");
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetButtonDown("Cheat"))
        {
            CarryOverInfo.carryOverInfoInstance.nextUnlockingLevels = new string[]{"Level 1","Level 2", "Level 3","Level 4", "Level 5","Level 6","Boss 1", "Boss 2", "Boss 3","Bunny","Bonus 1", "Bonus 2", "Bonus 3" };
            CarryOverInfo.carryOverInfoInstance.UnlockLevels();
        }

        if (Input.GetButtonDown("Cheat2"))
        {
            if (!GameObject.Find("Carry Over Info"))
                Instantiate(CarryOver);
        }

        if (Input.GetButtonDown("Cheat3"))
        {
            CarryOverInfo.carryOverInfoInstance.Save();
        }

        if (Input.GetButtonDown("Cheat4"))
        {
            SceneManager.LoadScene(5);
        }

        if (Input.GetButtonDown("Cheat5"))
        {
            CarryOverInfo.carryOverInfoInstance.nextUnlockingLevels = new string[4];
            for (int i = 0; i < 4; i++)
            {
                if (oWController.choosenLevel.connections[i] != null)
                    CarryOverInfo.carryOverInfoInstance.nextUnlockingLevels[i] = oWController.choosenLevel.connections[i].name;
            }
            CarryOverInfo.carryOverInfoInstance.UnlockLevels();
        }
    }
}
