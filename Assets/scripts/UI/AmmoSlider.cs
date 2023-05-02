using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSlider : MonoBehaviour
{
    public Slider ammoSlider;
    public int maxAmmo = 100;
    public int ammoUsedPerShot = 1;

    private int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoSlider.maxValue = maxAmmo;
        ammoSlider.value = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            // Player fired a shot, so decrement ammo count
            currentAmmo -= ammoUsedPerShot;
            ammoSlider.value = currentAmmo;
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, maxAmmo);
        ammoSlider.value = currentAmmo;
    }
}