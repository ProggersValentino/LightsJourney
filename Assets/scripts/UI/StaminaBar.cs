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

[SerializeField] Image abImageCD;
[SerializeField] private gunBehav gunInfo;

//ability cooldown related
public bool isCD = false;
[SerializeField] private float CDTime;
private float CDTimer;
public bool abDetect; //gets called from player script



private void Awake()
{
    instance = this;
}
    
void Start()
{
       staminaBar.maxValue = stamina.stamina;
        healthBar.maxValue = hs.currentHealth;
        ammoSlider.maxValue = ammo.bulletsLeft;
        
        // textCD.gameObject.SetActive(false);
        abImageCD.fillAmount = 0f;

        CDTime = gunInfo.secondaryGun.TBShooting;
        CDTimer = CDTime; // ensuring that the timer is set
}

void Update()
{
    staminaBar.value = stamina.stamina;
    healthBar.value = hs.currentHealth;
    ammoSlider.value = ammo.bulletsLeft;
    
    if (abDetect)
    {
        applyCD();
        // Debug.Log("working");
    }

}
void applyCD()
{
        
    //subtrack time from when last called
    CDTimer -= Time.deltaTime;

    if (CDTimer <= 0f)
    {
        abDetect = false;
        abImageCD.fillAmount = 0f;
        CDTimer = CDTime;

    }
    else
    {
        // textCD.text = Mathf.RoundToInt(CDTimer).ToString();
        abImageCD.fillAmount = CDTimer / CDTime;
    }
        
}

}
