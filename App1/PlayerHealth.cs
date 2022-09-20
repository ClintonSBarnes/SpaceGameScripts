using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    int maxHealth = 100;
    public int Shield;
    int maxShield = 100;
    int damageTaken;
    RectTransform canvasRectMask;
    [SerializeField] Animator animator;

    Collider2D hitBox;


    private void Start()
    {
        //this sets up the health for the player.
        health = maxHealth;
        Shield = maxShield;


        //this gets references to the necessary components. 
        hitBox = GetComponent<Collider2D>(); ///to use later with health deduction.
        canvasRectMask = GetComponent<RectTransform>();
    }


    public int GetHealth()
    {
        return health;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "enemy")
        {
            BulletScript enemyBullet;
            enemyBullet = collision.GetComponent<BulletScript>();
            damageTaken = enemyBullet.GetDamage();
            animator.SetBool("TakeHit", true);

            if (Shield + damageTaken > damageTaken)
            {
                Shield -= damageTaken;

            }
            else if (Shield + damageTaken < damageTaken && Shield != 0)
            {
                damageTaken -= Shield;
                Shield = 0;
                health -= damageTaken;
            }
            else if (Shield <= 0)
            {
                health -= damageTaken;
            }
        }

        animator.SetBool("TakeHit", false);


    }

    public int GetPlayerHealth()
    {
        return health;
    }

    public int GetPlayerShield()
    {
        return Shield;
    }

}
