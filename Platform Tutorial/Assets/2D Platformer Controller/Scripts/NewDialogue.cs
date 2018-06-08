using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class NewDialogue : MonoBehaviour {

    public string firstParagraph, secondParagraph, thirdParagraph, fourthParagraph, fifthParagraph, sixthParagraph, seventhParagraph, eightParagraph, ninthParagraph, tenthParagraph;
    public Canvas canvas;
    public Text textObject;
    public Text leftAnswer, rightAnswer;
    public AudioClip dialogue1;
    public AudioClip dialogue2;
    public AudioClip dialogue3;
    public AudioClip dialogue4;
    public AudioClip dialogue5;
    public AudioClip dialogue6;
    public AudioClip dialogue7;
    public AudioClip dialogue8;
    public AudioClip dialogue9;
    public AudioClip dialogue10;
    public AudioClip dialogue11;
    public AudioClip dialogue12;
    public AudioClip dialogue13;
    public AudioClip dialogue14;
    public AudioClip dialogue15;
    public AudioClip dialogue16;
    public AudioClip dialogue17;
    public AudioClip dialogue18;
    public AudioClip dialogueintro;
    public AudioClip dialogueYes;
    public AudioClip dialogueNo;

    private float readTime = 3.0f;
    private bool waitingForAnswer, hasAnswered;
    private int numberOfNos;
    private AudioSource myAudioSource;

	void Start ()
    {
        canvas.enabled = false;
        leftAnswer.enabled = false;
        rightAnswer.enabled = false;
        waitingForAnswer = false;
        numberOfNos = 0;
        hasAnswered = false;
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
        if(waitingForAnswer && Input.GetKeyDown(KeyCode.A) && !hasAnswered || waitingForAnswer && Input.GetKeyDown("joystick button 0") && !hasAnswered)
        {
            PrintText(secondParagraph);
            myAudioSource = GetComponent<AudioSource>();
            myAudioSource.Stop();
            myAudioSource.PlayOneShot(dialogueYes, 1f);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformInputs>().canMove = true;
            hasAnswered = true;
            Invoke("PlayPositiveGM", 1.0f);
            Invoke("DisableAnswers", 1.1f);
            Invoke("DisableCanvas", 9.0f);
        }

        if (waitingForAnswer && Input.GetKeyDown(KeyCode.B) || waitingForAnswer && Input.GetKeyDown("joystick button 1"))
        {
            Debug.Log("pre " + numberOfNos);
            numberOfNos++;
            Debug.Log("post " + numberOfNos);
            myAudioSource = GetComponent<AudioSource>();
            myAudioSource.PlayOneShot(dialogueNo, 1f);

            //DisableAnswers();

            switch (numberOfNos)
            {
                case 1:
                    PrintText(thirdParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue2, 1f);
                    break;
                case 2:
                    PrintText(fourthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue3, 1f);
                    break;
                case 3:
                    PrintText(fifthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue4, 1f);
                    break;
                case 4:
                    PrintText(sixthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue5, 1f);
                    break;
                case 5:
                    PrintText(seventhParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue6, 1f);
                    break;
                case 6:
                    PrintText(eightParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue7, 1f);
                    break;
                case 7:
                    PrintText(ninthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue8, 1f);
                    break;
                case 8:
                    PrintText(tenthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue9, 1f);
                    break;
                case 9:
                    SceneManager.LoadScene(22);
                    break;
                default:
                    Debug.Log("Switch broke");
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasAnswered)
        {
            //Stoppa spelaren
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformInputs>().canMove = false;
            canvas.enabled = true;
            PrintText(firstParagraph);
            myAudioSource = GetComponent<AudioSource>();
            myAudioSource.Stop();
            myAudioSource.PlayOneShot(dialogueintro, 1f);
        }

        else
            return;
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

    private void PlayPositiveGM()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(dialogue1, 1f);
    }

    private void DisableCanvas()
    {
        canvas.enabled = false;
        waitingForAnswer = false;
    }
}
