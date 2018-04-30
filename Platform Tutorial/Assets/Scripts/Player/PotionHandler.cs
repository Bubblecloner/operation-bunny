using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHandler : MonoBehaviour {

    public int maxPotions;

    private int[] potions;

	void Start ()
    {
        potions = new int[maxPotions];
	}
	
	void Update ()
    {
        GameController.gameControllerInstance.potions = potions;
	}

    public void DrinkPotion()
    {
        switch (potions[0])
        {
            case (0):
                //empty
                GetComponent<PlayerVariables>().Harm(1, 0, 0, gameObject);
                break;

            case (1):
                //standard healing
                transform.GetComponent<PlayerVariables>().Heal(100);
                break;

            case (2):
                //invincibility 
                GetComponent<PlayerVariables>().Invincible(10);
                break;

            case (3):
                //bonus damage
                GetComponent<PlayerVariables>().TempDamageIncrease(1,30);
                break;

            default:
                goto case (0);
        }

        potions[0] = 0;
        PotionDefrag();
    }

    public void SwapPotions()
    {
        int[] temp = new int[maxPotions];
        temp[0] = potions[0];

        for(int i = 0; i < maxPotions - 1; i++)
        {
            temp[i+1] = potions[i+1];
            potions[i] = temp[i+1];
        }
        potions[maxPotions-1] = temp[0];

        PotionDefrag();
    }

    private void PotionDefrag()
    {
        for(int i = 0; i < maxPotions; i++)
        {
            if(potions[i] == 0)
            {
                for(int a = i + 1; a < maxPotions; a++)
                {
                    if(potions[a] != 0)
                    {
                        potions[i] = potions[a];
                        potions[a] = 0;
                        break;
                    }
                }
            }
        }
    }

    public int[] Potions
    {
        set
        {
            if (value.Length == potions.Length)
                potions = value;
            else
                Debug.Log(value.ToString() + "Is not a proper value");

            PotionDefrag();
        }
    }
}
