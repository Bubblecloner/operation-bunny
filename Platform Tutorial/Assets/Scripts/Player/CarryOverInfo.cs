using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryOverInfo : MonoBehaviour {

    public static CarryOverInfo carryOverInfoInstance;
    public int maxPotions = 3;
    public Vector2 choosenLevel;
    public List<string> unlockedLevels;
    private int[] pots;
    private int saveNumber;
    public string[] nextUnlockingLevels;

    private Vector2 gravity;

	void Start ()
    {
        unlockedLevels.Add("Intro");
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
        if (level == 5)
        {
            UnlockLevels();
        }

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
            pots = pots,
            unlockedLevels = unlockedLevels
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

    private void UnlockLevel(string name)
    {
        bool unlock = true;
        for(int i = 0; i < unlockedLevels.Count; i++)
        {
            if (name == unlockedLevels[i])
            {
                unlock = false;
                break;
            }
        }
        if (unlock)
        {
            unlockedLevels.Add(name);
            if (GameObject.Find(name))
                GameObject.Find(name).GetComponent<OverworldLevel>().Fade();
        }
        
    }

    public void UnlockLevels()
    {
        for (int i = 0; i < nextUnlockingLevels.Length; i++)
            UnlockLevel(nextUnlockingLevels[i]);
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
