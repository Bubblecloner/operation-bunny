using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndingSceneScript : MonoBehaviour
{

    public Canvas canvas;
    public Text textObject;
    public string[] dialogue;
    public AudioClip[] sounds;
    public AudioClip explosionSound, bunnySound;
    public GameObject explosions;
    public GameObject gary;
    public Image fadeImage;

    private float garyMoveTime;
    private Vector3 garyDestination;
    private int dialogueNumber;
    private AudioSource audio;
    private bool garyLeft;

    void Start()
    {
        garyLeft = false;
        explosions.SetActive(false);
        audio = GetComponent<AudioSource>();
        dialogueNumber = 0;
        Invoke("Part1", 1.0f);
        Invoke("Part2", 6.0f);
        Invoke("Part3", 29.0f);
        Invoke("Part4", 36.0f);
    }

    private void Part1()
    {
        EnableCanvas();
        float duration = sounds[0].length;
        PlaySound(0);
        PrintText("Aaaaaaaaaargh… Gary!", Color.black, duration);
        // Move gary
        garyDestination = new Vector3(-2.2f, -4.06f, 0);
        garyMoveTime = 2.0f;
        garyLeft = false;
        Invoke("MoveGary", 2.0f);
    }

    private void Part2()
    {
        float duration = sounds[1].length;
        PlaySound(1);
        PrintText("Gary: Yes, Grand master? \nGM: Go find the bunny and let her kill you \nGary: Right, go kill the bunny. \nGM: No!Let her kill you! \nGary: Go get killed by the bunny.Right, got it. \nGM: You understand, Gary ? \nGary : Yes, grand master!", Color.black, duration + 3.0f);
        Invoke("DisableCanvas", duration + 5.0f);
        // Move gary
        garyDestination = new Vector3(-12.2f, -4.06f, 0);
        garyMoveTime = 8.0f;
        garyLeft = true;
        Invoke("MoveGary", duration + 2.0f);
    }

    private void Part3()
    {
        EnableCanvas();
        float duration = sounds[2].length;
        PlaySound(2);
        PrintText("Even Gary knew what I fucking meant…", Color.black, duration);
        Invoke("DisableCanvas", duration + 2.0f);
        Invoke("PlayBunnySound", duration + 3.5f);
    }

    private void Part4()
    {
        garyDestination = new Vector3(-2.2f, -4.06f, 0);
        garyMoveTime = 3.0f;
        garyLeft = false;
        Invoke("MoveGary", 0.0f);
        EnableCanvas();
        float duration = sounds[3].length;
        PlaySound(3);
        PrintText("GM: Gary… did you kill the bunny?\nGary: Yes.\nGM: Fuck’s sake.You were supposed to be killed by the bunny!\nGary: I was supposed to be what now ? ", Color.black, duration + 2.0f);
        Invoke("DisableCanvas", duration + 2.0f);
        Invoke("Explosions", duration + 2.1f);
    }

    private void Explosions()
    {
        explosions.SetActive(true);
        audio.PlayOneShot(explosionSound);
        FadeToBlack();
        Invoke("LoadNextScene", 3.0f);
    }

    private void PrintText(string textToPrint, Color textColor, float duration)
    {
        textObject.text = "";
        textObject.color = textColor;
        textObject.DOText(textToPrint, duration);
    }

    private void PlaySound(int index)
    {
        audio.PlayOneShot(sounds[index]);
    }

    private void MoveGary()
    {
        if (garyLeft)
        {
            gary.transform.localScale = new Vector3(-0.1335431f, 0.1335431f, 0.1335431f);
        }
        else
        {
            gary.transform.localScale = new Vector3(0.1335431f, 0.1335431f, 0.1335431f);
        }

        gary.GetComponent<GaryEnding>().Movement(garyDestination, garyMoveTime);
    }

    private void DisableCanvas()
    {
        canvas.enabled = false;
    }

    private void EnableCanvas()
    {
        canvas.enabled = true;
    }

    private void FadeToBlack()
    {
        fadeImage.DOFade(1, 1.5f);
    }

    private void PlayBunnySound()
    {
        audio.PlayOneShot(bunnySound);
    }

    private void LoadNextScene()
    {
        // Ändra scennummer här!
        SceneManager.LoadScene(20);
    }
}
