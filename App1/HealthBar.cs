using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth player;
    public Slider healthSlider;
    public Slider shieldSlider;

    private void Update()
    {
        healthSlider.value = player.GetHealth();
        shieldSlider.value = player.GetPlayerShield();
    }



}
