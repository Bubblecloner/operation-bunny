using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    public float health;
    public float damage;
    public Color damagedColor;
    public Transform target;
    public float speed = 3f;
    public float chaseRange;


    private SpriteRenderer sp;
    private bool foundPlayer = false;

    void OnEnable()
    {
        sp = GetComponent<SpriteRenderer>();
        //float distanceToTarget = Vector3.Distance(transform.position, target.position);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    

    void Update()
    {

        

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget < chaseRange)
        {
            foundPlayer = true;
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (foundPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }


        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        
   
    
    }



    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(0.225f);

        sp.color = new Color(1.000f, 1.000f, 1.000f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            ProjectileBehaviour projBehaviour = collision.gameObject.GetComponent<ProjectileBehaviour>();
            health -= projBehaviour.damage;
            sp.color = damagedColor;
            StartCoroutine(Waiter());
            Destroy(projBehaviour.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            health = -1;
            target.gameObject.GetComponent<Health>().TakeDamage();

        }
    }

   
}
