using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryOverInfo : MonoBehaviour {

    public static CarryOverInfo carryOverInfoInstance;
    public int maxPotions = 3;
    private int[] pots = { 0, 0, 0 };

    private Vector2 gravity;

	void Start ()
    {
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
    }

    public void UpdatePlayerStats(GameObject player)
    {
        Potions = player.GetComponent<PotionHandler>().Potions;
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
