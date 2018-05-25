using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bunny : MonoBehaviour
{

    public Canvas canvas;
    public Text leftAnswer, rightAnswer;

    private bool waitingForAnswer;
    private int numberOfNos;


    void Start()
    {
        canvas.enabled = false;
        leftAnswer.enabled = false;
        rightAnswer.enabled = false;
        waitingForAnswer = false;

    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            canvas.enabled = true;
        }


        if (waitingForAnswer && Input.GetKeyDown(KeyCode.A) || waitingForAnswer && Input.GetKeyDown("joystick button 0"))
        {
            DisableAnswers();

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stoppa spelaren
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformInputs>().canMove = false;
        canvas.enabled = true;
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