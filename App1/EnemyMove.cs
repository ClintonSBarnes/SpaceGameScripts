using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    [SerializeField] float hitRadius;
    [SerializeField] float chaseRadius;
    [SerializeField] float shootRadius;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask Player;
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform playerTransform;

    [SerializeField] float shootFrequencey = 1f;
    [SerializeField] float aimSpeed;





    [SerializeField] bool playerInChaseRadius;
    [SerializeField] bool playerInShootRadius;

    Vector3 playerTarget;
    Vector3 aimDirection;

    public float lastShot;
    public bool canFire;
    public bool chase;
    Rigidbody2D rb;



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, shootRadius);

    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerTarget = (playerTransform.position - transform.position).normalized;
        Shoot();
        ChaseRadiusCheck();
        ShootRadius();
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

    void ShootRadius()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, shootRadius, new Vector2(0f, 0f), shootRadius, Player);

        if (hit.collider != null && !playerInShootRadius)
        {
            playerInShootRadius = true;
        }
        else if (hit.collider == null && playerInShootRadius)
        {
            playerInShootRadius = false;
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
            lastShot = Time.time;
            rb.AddForce(new Vector2(playerTarget.x, playerTarget.y + rng) * moveSpeed, ForceMode2D.Force);
        }

        if (!playerInChaseRadius)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }



    //shoots at player when inside of radius


    void Shoot()
    {
        if (playerInShootRadius)
        {
            canFire = true;
        }
        if (!playerInShootRadius)
        {
            canFire = false;
        }


        if (canFire)
        {
            StartCoroutine(ShootDelay());



        }
    }

    IEnumerator ShootDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(shootFrequencey);
        float rng = Random.Range(0, 2) * 0.1f;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(new Vector2(playerTarget.x, playerTarget.y + rng) * bulletSpeed, ForceMode2D.Force);
    }
}


//need to work on shooting frequency