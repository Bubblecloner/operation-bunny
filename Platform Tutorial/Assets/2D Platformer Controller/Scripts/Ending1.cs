using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ending1 : MonoBehaviour {


    public string firstParagraph, secondParagraph, thirdParagraph, fourthParagraph, fifthParagraph, sixthParagraph, seventhParagraph, eightParagraph, ninthParagraph, tenthParagraph;
    public Canvas canvas;
    public Text textObject;
    public Text middleanswer;
    public AudioClip dialogue1;
    public AudioClip dialogue2;
    public AudioClip dialogue3;
    public AudioClip dialogue4;
    public AudioClip dialogue5;
    public AudioClip dialogue6;
    public AudioClip dialogue7;
    public AudioClip dialogue8;


    private float readTime = 3.0f;
    private bool waitingForAnswer;
    private AudioSource myAudioSource;

    void Start()
    {
        canvas.enabled = false;
        middleanswer.enabled = false;
        waitingForAnswer = false;
    }

    private void Update()
    {

        // Får svar från spelaren
        if (waitingForAnswer && Input.GetKeyDown(KeyCode.Space) || waitingForAnswer && Input.GetKeyDown("joystick button 0"))
        {
            DisableAnswers();
            myAudioSource = GetComponent<AudioSource>();
            myAudioSource.Stop();
        

            //DisableAnswers();

            switch (numberOfNos)
            {
                case 1:
                    PrintText(thirdParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue1, 1f);
                    break;
                case 2:
                    GetGary();
                    break;
                case 3:
                    PrintText(fourthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue2, 1f);
                    break;
                case 4:
                    PrintText(fifthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue3, 1f);
                    break;
                case 5:
                    PrintText(sixthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue4, 1f);
                    break;
                case 6:
                    PrintText(seventhParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue5, 1f);
                    break;
                case 7:
                    PrintText(eightParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue6, 1f);
                    break;
                case 8:
                    PrintText(ninthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue7, 1f);
                    break;
                case 9:
                    PrintText(tenthParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue8, 1f);
                    break;
                default:
                    Debug.Log("Switch broke");
                    break;
            }
        }
    }

    private void GetGary()
    {
        //Animera gary och vänta, gå sen igenom dialogen (ändra 1.0f till tiden det tar för animationen)
        StartCoroutine("GaryDialogue", 1.0f);
    }

    private IEnumerator GaryDialogue()
    {
        for (int i = 0; i < 8; i++)
        {
            switch (i)
            {
                case 0:
                    PrintText("Aaaaaaaaaargh… Gary!");
                    yield return new WaitForSeconds(4.0f);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue10, 1f);
                    break;
                case 1:
                    PrintText("Yes, Grand master ?");
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue11, 1f);
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 2:
                    PrintText("Go find the bunny and let her kill you");
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue12, 1f);
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 3:
                    PrintText("Right, go kill the bunny.");
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue13, 1f);
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 4:
                    PrintText("No! Let her kill you!");
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue14, 1f);
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 5:
                    PrintText("Go get killed by the bunny. Right, got it.");
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue15, 1f);
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 6:
                    PrintText("You understand, Gary?");
                    yield return new WaitForSeconds(4.0f);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue16, 1f);
                    break;
                case 7:
                    PrintText("Yes, grand master!");
                    yield return new WaitForSeconds(4.0f);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.Stop();
                    myAudioSource.PlayOneShot(dialogue17, 1f);
                    // Animera Gary en gång till här, och sen en till switch när han kommer tillbaka
                    break;
                default:
                    Debug.Log("Switch 2 broke");
                    break;
            }
        }

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stoppa spelaren
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformInputs>().canMove = false;
        canvas.enabled = true;
        PrintText(firstParagraph);
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(dialogueintro, 1f);
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
