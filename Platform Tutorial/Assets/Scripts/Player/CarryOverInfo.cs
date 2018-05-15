using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryOverInfo : MonoBehaviour {

    public static CarryOverInfo carryOverInfoInstance;
    public int maxPotions = 3;
    public Vector2 choosenLevel;
    private int[] pots;
    private int saveNumber;

    private Vector2 gravity;

	void Start ()
    {
        pots = new int[maxPotions];
        gravity = Physics2D.gravity;
        carryOverInfoInstance = this;
        DontDestroyOnLoad(gameObject);
	}
	
	void Update ()
    {

    }

    private void OnLevelWasLoaded(int level)
    {
        Physics2D.gravity = gravity;
        Save();
    }

    public void UpdatePlayerStats(GameObject player)
    {
        Potions = player.GetComponent<PotionHandler>().Potions;
    }

    public void Save()
    {
        SaveData Save = new SaveData
        {
            saveNumber = saveNumber,
            choosenLevelX = choosenLevel.x,
            choosenLevelY = choosenLevel.y,
            pots = pots
        };

        SaveLoad.Save(Save);
    }

    public void Load()
    {
        SaveData Save = SaveLoad.Load(saveNumber);
        if(Save != null)
        {
            choosenLevel = new Vector2(Save.choosenLevelX, Save.choosenLevelY);
            pots = Save.pots;
        }
    }



    public int[] Potions
    {
        get
        {
            int[] temp = new int[maxPotions];
            for(int i = 0; i < maxPotions; i++)
            {
                temp[i] = pots[i];
            }
            return temp;
        }
        private set
        {
            pots = value;
        }
    }
}
