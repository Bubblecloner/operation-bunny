using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite heartFull;
    public Sprite heartEmpty;



    public GameManager gm;

    private AudioSource audiomanager;
    public AudioClip[] audioclips;

    private bool hasDied;


    void Start ()
    {
        health = 5;
        audiomanager = GetComponent<AudioSource>();
        hasDied = false;
	}
	
	
	void Update ()
    {

        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        //if(health < 1)
        //{
        //    Invoke("LoadNextLevel", 1.0f);
            
        //}


        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = heartFull;
            }
            else
            {
                hearts[i].sprite = heartEmpty;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
	}


    //private void PlayerDead()
    //{
    //    PlayerSound(1);

    //    //audiomanager.clip = audioclips[1];
    //    //audiomanager.Play();
    //}
 

    public void TakeDamage()
    {
        health--;

        if(health >= 1)
        {
            audiomanager.PlayOneShot(audioclips[0]);
        }

        if(health <= 0 && !hasDied)
        {
            audiomanager.PlayOneShot(audioclips[1]);
            //Invoke("LoadNextLevel", 1.0f);
            gm.GameOverTryAgain();
            hasDied = true;
        }



        //PlayerSound(0);

        //audiomanager.clip = audioclips[0];
        //audiomanager.Play();
    }


    //private void LoadNextLevel()
    //{
    //    //PlayerDead();
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    //private void PlayerSound(int soundNumber)
    //{
    //    audiomanager.clip = audioclips[soundNumber];
    //    audiomanager.Play();
    //}

}
