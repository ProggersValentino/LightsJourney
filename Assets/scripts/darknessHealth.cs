using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darknessHealth : MonoBehaviour
{
    public healthMan stats;
    public float damage;
    public float currentHealth;

    private ParticleSystem ps;
    private BoxCollider shadowBox;
    
    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = stats.maxHealth;

        ps = GetComponentInChildren<ParticleSystem>();
        shadowBox = GetComponent<BoxCollider>();

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
        var main = ps.main;
        shadowBox.enabled = false;
        // Destroy(gameObject);
        main.loop = false;
    }

    
}
