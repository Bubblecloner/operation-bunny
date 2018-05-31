using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewDialogue : MonoBehaviour {

    public string firstParagraph, secondParagraph, thirdParagraph, fourthParagraph, fifthParagraph, sixthParagraph, seventhParagraph, eightParagraph, ninthParagraph, tenthParagraph;
    public Canvas canvas;
    public Text textObject;
    public Text leftAnswer, rightAnswer;
    public AudioClip dialogueTalk;

    private float readTime = 3.0f;
    private bool waitingForAnswer;
    private int numberOfNos;
    private AudioSource myAudioSource;

	void Start ()
    {
        canvas.enabled = false;
        leftAnswer.enabled = false;
        rightAnswer.enabled = false;
        waitingForAnswer = false;
        numberOfNos = 0;
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
        if(waitingForAnswer && Input.GetKeyDown(KeyCode.A) || waitingForAnswer && Input.GetKeyDown("joystick button 0"))
        {
            DisableAnswers();
            PrintText(secondParagraph);
        }

        if (waitingForAnswer && Input.GetKeyDown(KeyCode.B) || waitingForAnswer && Input.GetKeyDown("joystick button 1"))
        {
            Debug.Log("pre " + numberOfNos);
            numberOfNos++;
            Debug.Log("post " + numberOfNos);
            //DisableAnswers();

            switch (numberOfNos)
            {
                case 1:
                    PrintText(thirdParagraph);
                    myAudioSource = GetComponent<AudioSource>();
                    myAudioSource.PlayOneShot(dialogueTalk, 1f);
                    break;
                case 2:
                    PrintText(fourthParagraph);
                    break;
                case 3:
                    PrintText(fifthParagraph);
                    break;
                case 4:
                    PrintText(sixthParagraph);
                    break;
                case 5:
                    PrintText(seventhParagraph);
                    break;
                case 6:
                    PrintText(eightParagraph);
                    break;
                case 7:
                    PrintText(ninthParagraph);
                    break;
                case 8:
                    PrintText(tenthParagraph);
                    break;
                case 9:
                    GetGary();
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
                    break;
                case 1:
                    PrintText("Yes, Grand master ?");
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 2:
                    PrintText("Go find the bunny and let her kill you");
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 3:
                    PrintText("Right, go kill the bunny.");
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 4:
                    PrintText("No! Let her kill you!");
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 5:
                    PrintText("Go get killed by the bunny. Right, got it.");
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 6:
                    PrintText("You understand, Gary?");
                    yield return new WaitForSeconds(4.0f);
                    break;
                case 7:
                    PrintText("Yes, grand master!");
                    yield return new WaitForSeconds(4.0f);
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
