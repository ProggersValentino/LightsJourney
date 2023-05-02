using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

// Stamina
public Slider staminaBar;
public movement stamina;

//Health
public Slider healthBar;
[SerializeField] collision hs;

//ammo
public Slider ammoSlider;
public gunBehav ammo;

public static StaminaBar instance;



private void Awake()
{
    instance = this;
}
    
void Start()
{
       staminaBar.maxValue = stamina.stamina;
        healthBar.maxValue = hs.currentHealth;
        ammoSlider.maxValue = ammo.bulletsLeft;
}

void Update()
{
    staminaBar.value = stamina.stamina;
    healthBar.value = hs.currentHealth;
    ammoSlider.value = ammo.bulletsLeft;
}

}
