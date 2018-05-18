using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewDialogue : MonoBehaviour {

    public string firstParagraph, secondParagraph, thirdParagraph;
    public Canvas canvas;
    public Text textObject;
    public Text leftAnswer, rightAnswer;

    private float readTime = 3.0f;
    private bool waitingForAnswer;

	void Start ()
    {
        canvas.enabled = false;
        leftAnswer.enabled = false;
        rightAnswer.enabled = false;
        waitingForAnswer = false;
	}

    private void Update()
    {
        // Testar metoden
        if (Input.GetKeyDown(KeyCode.T))
        {
            canvas.enabled = true;
            PrintText(firstParagraph);
        }

        // Får svar från spelaren
        if(waitingForAnswer && Input.GetKeyDown(KeyCode.A))
        {
            DisableAnswers();
            PrintText(secondParagraph);
        }

        if (waitingForAnswer && Input.GetKeyDown(KeyCode.B))
        {
            DisableAnswers();
            PrintText(thirdParagraph);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stoppa spelaren
        canvas.enabled = true;
        PrintText(firstParagraph);
    }

    private void PrintText(string textToPrint)
    {
        textObject.text = "";
        textObject.DOText(textToPrint, readTime);
        Invoke("EnableAnswers", readTime + 1.0f);
    }

    private void EnableAnswers()
    {
        leftAnswer.enabled = true;
        rightAnswer.enabled = true;
        waitingForAnswer = true;
    }

    private void DisableAnswers()
    {
        leftAnswer.enabled = false;
        rightAnswer.enabled = false;
        waitingForAnswer = false;
    }
}
