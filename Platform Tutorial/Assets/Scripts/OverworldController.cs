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
        if (CarryOverInfo.carryOverInfoInstance.choosenLevel != new Vector2(-800,-800))
            FindLevel(CarryOverInfo.carryOverInfoInstance.choosenLevel);


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
            else if (Input.GetButtonDown("Shop"))
            {
                EnterShop();
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

    private void FindLevel(Vector2 posistion)
    {
        choosenLevel = Physics2D.OverlapCircle(posistion,0.01f).GetComponent<OverworldLevel>();
    }

    private void StartLevel()
    {
        CarryOverInfo.carryOverInfoInstance.choosenLevel = choosenLevel.transform.position;
        for (int i = 0; i < choosenLevel.connections.Length; i++)
            if (choosenLevel.connections[i] != null)
                CarryOverInfo.carryOverInfoInstance.nextUnlockingLevels = new string[] { choosenLevel.connections[i].name };
        SceneManager.LoadScene(choosenLevel.levelScene);
    }

    private void EnterShop()
    {
        CarryOverInfo.carryOverInfoInstance.choosenLevel = choosenLevel.transform.position;

        SceneManager.LoadScene(7);
    }
}
