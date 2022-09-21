using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingEnemyController : MonoBehaviour
{

    [SerializeField] float chaseRadius;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask Player;
    [SerializeField] GameObject childEnemyPrefab;
    [SerializeField] Transform playerTransform;


    [SerializeField] bool playerInChaseRadius;

    Vector3 playerTarget;
    Vector3 aimDirection;

    public bool chase;
    Rigidbody2D rb;



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerTarget = (playerTransform.position - transform.position).normalized;
        ChaseRadiusCheck();
        ChasePlayer();
    }




    void ChaseRadiusCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, chaseRadius, new Vector2(0f, 0f), chaseRadius, Player);

        if (hit.collider != null)
        {
            playerInChaseRadius = true;
        }
        else
        {
            playerInChaseRadius = false;
        }
    }


    //chase player when inside of radius
    void ChasePlayer()
    {
        if (playerInChaseRadius)
        {
            chase = true;
        }
        if (!playerInChaseRadius)
        {
            chase = false;
        }

        if (chase)
        {
            float rng = Random.Range(0, 2) * 0.1f;
            rb.AddForce(new Vector2(playerTarget.x, playerTarget.y + rng) * moveSpeed, ForceMode2D.Force);
        }

        if (!playerInChaseRadius)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Split();
        }
    }

    void Split()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(childEnemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

}


//Functional, but needs to have a related script that enables the follow functionality to the children following
//their instantiation. 