using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollision : MonoBehaviour
{
    public healthMan stats;
    public float damage;
    private float currentHealth;

    private void Start()
    {
        currentHealth = stats.maxHealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        Debug.Log(currentHealth);
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("dead");
            die();
        }
    }

    public void die()
    {
        GetComponent<LootBag>().spawnPU(transform.position);
        Destroy(gameObject);
    }
}
