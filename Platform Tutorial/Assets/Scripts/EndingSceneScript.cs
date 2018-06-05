using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndingSceneScript : MonoBehaviour {

    public Canvas canvas;
    public Text textObject;
    public string[] dialogue;
    public AudioClip[] sounds;
    public GameObject explosions;

    private float readTime, waitTime;
    private int dialogueNumber;
    private AudioSource audio;

	void Start ()
    {
        explosions.SetActive(false);
        audio = GetComponent<AudioSource>();
        dialogueNumber = 0;
        waitTime = 2.0f;
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
    }

    private void Part2()
    {
        float duration = sounds[1].length;
        PlaySound(1);
        PrintText("Gary: Yes, Grand master? \nGM: Go find the bunny and let her kill you \nGary: Right, go kill the bunny. \nGM: No!Let her kill you! \nGary: Go get killed by the bunny.Right, got it. \nGM: You understand, Gary ? \nGary : Yes, grand master!", Color.black, duration + 3.0f);
        Invoke("DisableCanvas", duration + 5.0f);
        // Move gary
    }

    private void Part3()
    {
        EnableCanvas();
        float duration = sounds[2].length;
        PlaySound(2);
        PrintText("Even Gary knew what I fucking meant…", Color.black, duration);
        Invoke("DisableCanvas", duration + 2.0f);
    }

    private void Part4()
    {
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
        dialogueNumber = 0;
        Debug.Log("Gary");
        // Animate gary moving
        Invoke("StartSecondDialogue", 3.0f);
    }

    private void DisableCanvas()
    {
        canvas.enabled = false;
    }

    private void EnableCanvas()
    {
        canvas.enabled = true;
    }
}
