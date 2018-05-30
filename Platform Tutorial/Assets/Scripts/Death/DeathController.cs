using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour {

    public GameObject introText;
    public GameObject rock;
    public GameObject paper;
    public GameObject scissors;
    public GameObject PlayerHand;
    public GameObject DeathHand;
    public float state2Timer = 2;


    private int gameState = 0;
    private int playerHand = 0;
    private int deathHand = 0;


    void Start () {
		
	}
	
	void Update ()
    {
        switch (gameState)
        {
            case 0:
                if (Input.GetButtonDown("Submit"))
                {
                    //add sound later
                    StartGame();
                }
                else if (Input.GetButtonDown("Cancel"))
                {
                    //add sound later
                }
                break;

            case 1:

                if (Input.GetButtonDown("Rock"))
                {
                    ChooseHand(1);
                }
                else if (Input.GetButtonDown("Paper"))
                {
                    ChooseHand(2);
                }
                else if (Input.GetButtonDown("Scissors"))
                {
                    ChooseHand(3);
                }
                break;

            case 2:
                rock.SetActive(false);
                paper.SetActive(false);
                scissors.SetActive(false);

                PlayerHand.SetActive(true);
                DeathHand.SetActive(true);

                
                if (Time.frameCount % 3 == 0)
                    deathHand = (Random.Range(0, 3) + 1);

                switch (deathHand)
                {
                    case 1:
                        DeathHand.GetComponent<Text>().text = "Rock";
                        break;


                    case 2:
                        DeathHand.GetComponent<Text>().text = "Paper";
                        break;


                    case 3:
                        DeathHand.GetComponent<Text>().text = "Scissors";
                        break;
                }

                state2Timer -= Time.deltaTime;
                if (state2Timer <= 0)
                    gameState = 3;
                break;

            case 3:
                Invoke("CheckWin", 1.5f);
                break;
        }
	}

    public void StartGame()
    {
        introText.SetActive(false);
        gameState = 1;
        rock.SetActive(true);
        paper.SetActive(true);
        scissors.SetActive(true);
    }

    public void ChooseHand(int hand)
    {
        playerHand = hand;
        gameState = 2;
        switch (hand)
        {
            case 1:
                PlayerHand.GetComponent<Text>().text = "Rock";
                break;


            case 2:
                PlayerHand.GetComponent<Text>().text = "Paper";
                break;


            case 3:
                PlayerHand.GetComponent<Text>().text = "Scissors";
                break;
        }
    }

    private void CheckWin()
    {

        if (playerHand != 0)
        {
            //Sets deathHand to a random integer between 1-3
            

            switch (playerHand - deathHand)
            {
                case -2:
                    Win();
                    break;

                case -1:
                    Lose();
                    break;

                case 0:
                    Tie();
                    break;

                case 1:
                    Win();
                    break;

                case 2:
                    Lose();
                    break;

                default:
                    goto case 2;
            }
        }
    }

    private void Win()
    {
        GameController.gameControllerInstance.DeathHeal();
        SceneManager.UnloadSceneAsync(15);
    }

    private void Lose()
    {
        GameController.gameControllerInstance.ReloadLevel();
        SceneManager.UnloadSceneAsync(15);
    }

    private void Tie()
    {
        Lose();
    }
}
