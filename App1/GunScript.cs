using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Intakes:")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] PlayerInput input;
    [SerializeField] PlayerAim aim;
    [SerializeField] GameObject gun;


    [Header("Adjustable:")]
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float fireRate = 1f;

    [Header("Bool Checks:")]
    [SerializeField] bool canFire = true;
    [SerializeField] bool isShooting = false;

    float lastShootTime;


    private void FixedUpdate()
    {
        Shoot();
        FireCheck();
    }

    void Shoot()
    {
        FireCheck();

        if (isShooting && canFire)
        {
            lastShootTime = Time.time;
            canFire = false;
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, aim.transform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);

        }
        if (!canFire)
        {
            if (Time.time > lastShootTime + fireRate)
            {
                canFire = true;
            }
        }
    }


    void FireCheck()
    {
        if (input.GetShootPressed())
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }


}
