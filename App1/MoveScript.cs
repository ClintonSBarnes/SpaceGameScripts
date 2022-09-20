using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveScript : MonoBehaviour
{
    [Header("Script Intake:")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Transform player;
    [Header("Adjustments")]
    [SerializeField] float defaultMoveSpeed = 5f;
    [SerializeField] float sprintMoveSpeed = 10f;
    [SerializeField] float gravity;
    [SerializeField] float boostForce;
    [SerializeField] int boostAmount; //this is like fuel - it is a passive cool down mechanisim
    [SerializeField] float noBoost = 0;
    [SerializeField] float boostCoolDownTime = 1;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask ceilingMask;
    [SerializeField] Transform groundCheckTransform;


    [Header("Status:")]
    [SerializeField] bool isGrounded;
    [SerializeField] bool isHittingCeiling;
    public bool isBoosting = false;
    public bool isFlipped;


    //Hidden Variables:
    float currentMoveSpeed;
    float currentBoost;
    float lastBoostTime;
    Animator animator;

    void Start()
    {
        player = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Move();
        Sprinting();
        Boost();
        NewGroundCheck();
        CeilingCheck();
        Flip();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.position, groundCheckRadius);
    }

    private void Move()
    {

        if (!isFlipped)
        {
            if (!isGrounded && !isHittingCeiling && isBoosting) // using jetpack (not grounded) WITHOUT SPRINT (if you use sprint that will have to be added
            {
                player.Translate(new Vector3(playerInput.GetPlayerDirectionInput().x * defaultMoveSpeed, currentBoost * gravity));
            }
            if (!isGrounded && !isHittingCeiling && !isBoosting) // using jetpack (not grounded) WITHOUT SPRINT (if you use sprint that will have to be added
            {
                player.Translate(new Vector3(playerInput.GetPlayerDirectionInput().x * defaultMoveSpeed, gravity));
            }
            if (!isGrounded && isHittingCeiling)
            {
                player.Translate(new Vector3(playerInput.GetPlayerDirectionInput().x * defaultMoveSpeed, gravity));
            }
            /*if (!isGrounded && playerInput.isSprintPressed) // using jetpack (not grounded) WITH SPRINT
            {
                player.Translate(new Vector3(playerInput.playerDirection.x * currentMoveSpeed, currentBoost * gravity));
            }*/

            if (isGrounded && !playerInput.GetJumpPressed()) // grounded and not jumping
            {
                player.Translate(new Vector3(playerInput.GetPlayerDirectionInput().x * currentMoveSpeed, 0f));
            }
            if (isGrounded && playerInput.GetJumpPressed()) // grounded and player jumps
            {
                player.Translate(new Vector3(playerInput.GetPlayerDirectionInput().x * currentMoveSpeed, currentBoost * gravity));
            }


            if (!isGrounded)
            {
                animator.SetBool("Walking", true);
            }

        }

        if (isFlipped)
        {
            if (!isGrounded && !isHittingCeiling && !isBoosting) // using jetpack (not grounded) WITHOUT SPRINT (if you use sprint that will have to be added
            {
                player.Translate(new Vector3(-playerInput.GetPlayerDirectionInput().x * defaultMoveSpeed, gravity));
            }
            if (!isGrounded && !isHittingCeiling && isBoosting) // using jetpack (not grounded) WITHOUT SPRINT (if you use sprint that will have to be added
            {
                player.Translate(new Vector3(-playerInput.GetPlayerDirectionInput().x * defaultMoveSpeed, currentBoost * gravity));
            }
            if (!isGrounded && isHittingCeiling)
            {
                player.Translate(new Vector3(-playerInput.GetPlayerDirectionInput().x * defaultMoveSpeed, gravity));
            }
            /*if (!isGrounded && playerInput.isSprintPressed) // using jetpack (not grounded) WITH SPRINT
            {
                player.Translate(new Vector3(playerInput.playerDirection.x * currentMoveSpeed, currentBoost * gravity));
            }*/

            if (isGrounded && !playerInput.GetJumpPressed()) // grounded and not jumping
            {
                player.Translate(new Vector3(-playerInput.GetPlayerDirectionInput().x * currentMoveSpeed, 0f));
            }
            if (isGrounded && playerInput.GetJumpPressed()) // grounded and player jumps
            {
                player.Translate(new Vector3(-playerInput.GetPlayerDirectionInput().x * currentMoveSpeed, currentBoost * gravity));
            }


            if (!isGrounded && currentMoveSpeed == defaultMoveSpeed)
            {
                animator.SetBool("Walking", true);
            }
            else if (currentMoveSpeed == sprintMoveSpeed)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
                animator.SetBool("Walking", false);
            }
        }
    }

    void Sprinting()
    {
        if (playerInput.GetSprintPressed())
        {
            currentMoveSpeed = sprintMoveSpeed;

        }
        else
        {
            currentMoveSpeed = defaultMoveSpeed;
        }
    }

    void Boost()
    {
        if (playerInput.GetJumpPressed() && boostAmount > 0)
        {
            isBoosting = true;
            lastBoostTime = Time.time;
            boostAmount -= 1;
            currentBoost = boostForce;
        }
        else if (!playerInput.GetJumpPressed() && boostAmount < 100)
        {
            isBoosting = false;
            if (Time.time > lastBoostTime + boostCoolDownTime)
            {
                boostAmount += 1;
            }

        }
        else
        {
            isBoosting = false;
            currentBoost = noBoost;
        }

        if (isBoosting)
        {
            animator.SetBool("Flying", true);
        }
        else if (!isGrounded && !isBoosting)
        {
            animator.SetBool("Flying", false);
            animator.SetBool("Floating", true);
        }
        else
        {
            animator.SetBool("Flying", false);
            animator.SetBool("Floating", false);
        }

    }

    public int GetBoostAmount()
    {
        return boostAmount;
    }


    void NewGroundCheck()
    {
        if (Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    void CeilingCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(player.position, groundCheckRadius, new Vector2(0f, 0f), groundCheckRadius, ceilingMask);

        if (hit.collider != null)
        {
            isHittingCeiling = true;
        }
        else
        {
            isHittingCeiling = false;
        }
    }

    void Flip()
    {
        if (playerInput.GetPlayerDirectionInput().x > -0.25f && playerInput.GetPlayerDirectionInput().x < 0.25f)
        {

        }
        else if (playerInput.GetPlayerDirectionInput().x < -0.25f)
        {
            isFlipped = true;
            transform.rotation = new Quaternion(0f, -180f, 0f, 0f);
        }
        else if (playerInput.GetPlayerDirectionInput().x > 0.25f)
        {
            isFlipped = false;
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    public bool GetGrounded()
    {
        return isGrounded;
    }

    public bool GetHittingCeiling()
    {
        return isHittingCeiling;
    }

    public bool GetBoosting()
    {
        return isBoosting;
    }

    public bool GetFlipped()
    {
        return isFlipped;
    }

}

//BOOST IS A BIT BROKEN - LIKELY A FIXED UPDATE ISSUE.

/*

 */
