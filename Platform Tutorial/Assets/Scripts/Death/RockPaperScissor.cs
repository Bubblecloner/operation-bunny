using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPaperScissor : MonoBehaviour {

    private int playerHand = 0;
    private int deathHand = 0;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (playerHand == 0)
        {
            //input to change playerHand

            if (playerHand != 0)
            {
                //Sets deathHand to a random integer between 1-3
                deathHand = (int)(Random.Range(0, 1) * 3 + 1);
                
                switch (playerHand - deathHand)
                {
                    case -2:
                        //Win
                        break;

                    case -1:
                        //Lose
                        break;

                    case 0:
                        //Tie
                        break;

                    case 1:
                        //Win
                        break;

                    case 2:
                        //Lose
                        break;
                }
            }
        }

	}
}
