using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JetPackEmission : MonoBehaviour
{
    [SerializeField] MoveScript move;
    [SerializeField] ParticleSystem particleSystem;




    void FixedUpdate()
    {


        if (move.GetBoosting())
        {
            var emission = particleSystem.emission;
            emission.rate = 265f;
        }
        else
        {
            var emission = particleSystem.emission;
            emission.rate = 0f;
        }
    }


}
