using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldController : MonoBehaviour {
    
    public GameObject player;
    public OverworldLevel choosenLevel;
    public float moveTime = 1;

    private Transform moveTarget;
    private bool moving = false;
    private float moveTimer;

	void Start ()
    {
        player.transform.position = choosenLevel.transform.position;
	}
	
	void Update ()
    {
        if (!moving)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 && choosenLevel.connections[0])
            {
                StartMove(choosenLevel.connections[0]);
            }
            else if (Input.GetAxisRaw("Vertical") > 0 && choosenLevel.connections[1])
            {
                StartMove(choosenLevel.connections[1]);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0 && choosenLevel.connections[2])
            {
                StartMove(choosenLevel.connections[2]);
            }
            else if (Input.GetAxisRaw("Vertical") < 0 && choosenLevel.connections[3])
            {
                StartMove(choosenLevel.connections[3]);
            }
            else if (Input.GetButtonDown("Submit"))
            {
                StartLevel();
            }
        }
        else
        {
            MovingPlayer();
        }

	}

    private void StartMove(Transform target)
    {
        if (target.GetComponent<OverworldLevel>().unlocked)
        {
            moveTimer = 0;
            moveTarget = target;
            moving = true;
        }
    }

    private void MovingPlayer()
    {
        player.transform.position = Vector2.Lerp(choosenLevel.transform.position, moveTarget.position, moveTime * moveTimer);
        if (moveTimer*moveTime >= 1)
        {
            moving = false;
            choosenLevel = moveTarget.GetComponent<OverworldLevel>();
        }
        moveTimer += Time.deltaTime;
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(choosenLevel.levelScene);
    }
}
