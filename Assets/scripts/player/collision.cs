using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class collision : MonoBehaviour
{
    public healthMan stats;
    public float darkDmg;
    public float physicalDmg;
    public float currentHealth;
    public float dmgTick;
    
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

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("dead");
            die();
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }

    IEnumerator darkOT()
    {
        yield return new WaitForSeconds(dmgTick);
        TakeDamage(darkDmg);

        
    }
}
