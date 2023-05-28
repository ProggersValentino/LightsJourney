using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollision : MonoBehaviour
{
    public healthMan stats;
    public float damage;
    private float currentHealth;

    // //audio related
    // private NavMeshAI AITings;
    // private int audioIndex = -1;
    // private AudioManager audManger;

    public GameObject deathEffect;
    
    

    // private void Awake()
    // {
    //     // audManger = FindObjectOfType<AudioManager>();
    // }

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
        if (gameObject.CompareTag("Enemy"))
        {
            GetComponent<LootBag>().spawnPU(transform.position);
            Instantiate(deathEffect, transform.position, Quaternion.identity); //instantiates particle effect as well as sound
        }
        Destroy(gameObject);
    }
}
