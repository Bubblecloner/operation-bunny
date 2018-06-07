using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public Projectile proj;


    private string tpDir;
    private float tpCD = 2.5f;
    private bool canTP = true;


    Animator myanim;



    void Start()
    {
        myanim = GetComponent<Animator>();

        proj = gameObject.GetComponent<Projectile>();
        tpDir = "Down";
    }


    void Update()
    {
        //Right
        if (Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            myanim.SetBool("walking_right", true);
            myanim.SetBool("walking_up", false);
            myanim.SetBool("walking_down", false);
            myanim.SetBool("walking_left", false);
            tpDir = "Right";

            proj.velocity = new Vector2(5, 0f);

        }

        //Left
        if (Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            myanim.SetBool("walking_left", true);
            myanim.SetBool("walking_up", false);
            myanim.SetBool("walking_down", false);
            myanim.SetBool("walking_right", false);
            tpDir = "Left";


            proj.velocity = new Vector2(-5, 0f);
        }

        //Down
        if (Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            myanim.SetBool("walking_down", true);
            myanim.SetBool("walking_up", false);
            myanim.SetBool("walking_right", false);
            myanim.SetBool("walking_left", false);
            tpDir = "Down";

            proj.velocity = new Vector2(0f, -5f);
        }

        //Up
        if (Input.GetAxisRaw("Vertical") > 0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            myanim.SetBool("walking_up", true);
            myanim.SetBool("walking_right", false);
            myanim.SetBool("walking_left", false);
            myanim.SetBool("walking_down", false);
            tpDir = "Up";

            proj.velocity = new Vector2(0f, 5f);
        }


        if (Input.GetKey(KeyCode.LeftControl)&& Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.X)&& Input.GetKey(KeyCode.O) && canTP)
        {
            tp();
            StartCoroutine(CanTP());
        }



    }

    IEnumerator CanTP()
    {
        canTP = false;
        yield return new WaitForSeconds(tpCD);
        canTP = true;
    }
    

    private void tp()
    {
        if (tpDir.Equals("Up"))
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 5);
        }

        if (tpDir.Equals("Down"))
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 5);
        }

        if (tpDir.Equals("Left"))
        {
            transform.localPosition = new Vector2(transform.localPosition.x -5, transform.localPosition.y );
        }

        if (tpDir.Equals("Right"))
        {
            transform.localPosition = new Vector2(transform.localPosition.x +5, transform.localPosition.y);
        }
    }

}