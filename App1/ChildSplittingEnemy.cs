using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSplittingEnemy : MonoBehaviour
{

    Vector3 playerTarget;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    PlayerInput player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        FindPlayer();
    }

    void Update()
    {
        Chase();
    }


    void FindPlayer()
    {
        player = FindObjectOfType<PlayerInput>();
        playerTarget = player.transform.position;

    }

    void Chase()
    {
        playerTarget = player.transform.position;
        float rng = Random.Range(0, 2) * 0.1f;
        rb.AddForce(new Vector2(playerTarget.x, playerTarget.y + rng) * moveSpeed, ForceMode2D.Force);
    }
}

//instantiation is working, but need to refine the chase feature. Specifically, need to have
//aim update on fixed update. 