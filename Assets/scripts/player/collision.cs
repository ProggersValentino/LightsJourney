using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour
{
    public healthMan stats;
    public float darkDmg;
    public float physicalDmg;
    public float currentHealth;
    public float dmgTick;
    
    //regen related
    public float healthRegenTick;
    public float elapsedTime;
    public float delayPeriod;

    public SceneManage manageScene;



    //collection of keys 
    inventory keyColl;
    
    //editing movement
    private movement playerMStat;
    

    private void Awake()
    {
        currentHealth = stats.maxHealth;
        keyColl = GetComponent<inventory>();
        playerMStat = GetComponent<movement>();
    }

    private void Update()
    {
        passiveRegen();
    }

    //when in fog
   private void OnTriggerStay(Collider other)
   {
       if (other.CompareTag("Darkness"))
       {
           StartCoroutine(darkOT());
           // playerMStat.walkSped = 5;
           Debug.Log(currentHealth);
       }
       // else
       // {
       //     playerMStat.walkSped = 7;
       // }
   }
    
   //when interacting with keys
   private void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("key"))
       {
           keyColl.keys++;
           Destroy(other.gameObject);
       }
       // else if (other.CompareTag("k2"))
       // {
       //     keyColl.k2Found = true;
       //     Debug.Log(keyColl.k2Found);
       //     Destroy(other.gameObject);
       // }
       // else if (other.CompareTag("k3"))
       // {
       //     keyColl.k3Found = true;
       //     Debug.Log(keyColl.k3Found);
       //     Destroy(other.gameObject);
       // }
   }

    
   //when colliding physically with enemies
   private void OnCollisionEnter(Collision other)
   {
       if (other.collider.CompareTag("Enemy"))
       {
           TakeDamage(physicalDmg);
           Debug.Log(currentHealth);
       }
   }

   public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        //resetting elapse time
        elapsedTime = 0f;
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("dead");
            die();
        }
    }

   public void passiveRegen()
   {
       elapsedTime += Time.deltaTime;

       if (elapsedTime >= delayPeriod)
       {
           // Compare the current value with the initial value
           if (currentHealth < 100)
           {
               currentHealth += healthRegenTick;
           }
           else
           {
               elapsedTime = 0f;
           }
           // healthAtPoint = currentHealth; // Update the initial value for the next detection
       }
   }

    public void die()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(index);
    }

    IEnumerator darkOT()
    {
        yield return new WaitForSeconds(dmgTick);
        TakeDamage(darkDmg);
    }
}
