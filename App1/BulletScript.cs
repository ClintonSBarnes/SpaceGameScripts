using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] int defaultDamage = 10;
    string gunType1 = "enemyRaygun";
    string gunType2 = "enemyLaser";
    string gunType3;
    string gunType4;
    string gunType5;

    int type1Damage = 5;
    int type2Damage = 10;
    int type3Damage = 15;
    int type4Damage = 20;
    int type5Damage = 25;

    [SerializeField] string bulletTag;


    private void Update()
    {

    }
    public int GetDamage()
    {

        return defaultDamage;

    }
}

