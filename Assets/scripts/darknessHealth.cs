using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class darknessHealth : MonoBehaviour
{
    public healthMan stats;
    public float damage;
    public float currentHealth;

    private ParticleSystem ps;
    private BoxCollider shadowBox;

    public float regenAfterSec;
    public float timer = 0f;
    private bool fogDied = false;
    
    //event to trigger regen for fog 
    public delegate void FogTriggeredHandler();
    public static event FogTriggeredHandler FogTriggered; // Event for triggering the fog

    public timerManager manageDownSoldiers;
    
    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = stats.maxHealth; //setting the health to whatever is in stats 

        ps = GetComponentInChildren<ParticleSystem>();
        shadowBox = GetComponent<BoxCollider>();

        timer = regenAfterSec;

        FogTriggered += OnFogTriggered; //subscribed to OnfogTriggered
        
        // Debug.Log(FogTriggered?.GetInvocationList().Length);
    }
    
    //takes damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Debug.Log("dead");
            die();
        }
    }
    

    public void die()
    {
        // Debug.Log("die calling me");
        fogDied = true;
        //when the player kills the fog then these actions will happen
        var main = ps.main; //accessing the particle system 
        shadowBox.enabled = false;
        main.loop = false;
        manageDownSoldiers.managingTime.Add(new timeItem(this, regenAfterSec + Time.time));
    }
    
    //to regenerate fog
    public void OnFogTriggered()
    {
        var main = ps.main;
        
        // The cooldown time has elapsed, so trigger the fog and update the last trigger time
        shadowBox.enabled = true;
        main.loop = true;
        ps.Play();

        //reset health
        currentHealth = stats.maxHealth;

        // Debug.Log(Time.time);
    }
    
    // Method for triggering the fog externally
    public void TriggerFog()
    {
        // Raise the event for triggering the fog
        FogTriggered?.Invoke();
    }

    
}
