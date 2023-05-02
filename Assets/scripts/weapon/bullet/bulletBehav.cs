using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulletBehav : MonoBehaviour
{
    public BStatsSO bulletType;

    private float maxLifetime;

    private int collisions;
    
    PhysicMaterial phys_mat;

    void Start()
    {
        maxLifetime = bulletType.maxLifetime;
        Setup();    
    }
    void Update() 
    {
        //checking when to explode
        if (collisions > bulletType.maxCollisions)
        {
            explode();
        }

        //count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Debug.Log("time up");
            explode();
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        //count up collisions
        collisions++;
    
        // //explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Enemy") && bulletType.explodeOnTouch)
        {
            Debug.Log("explode");
            explode();
        }
        if (collision.collider.CompareTag("Player") && bulletType.explodeOnTouch)
        {
            Debug.Log("explode");
            explode();
        }
    
        // if (collision.collider.CompareTag("Enemy"))
        // {
        //     GetComponent<enemyCollision>().TakeDamage(bulletType.directDmg);
        //     Debug.Log("direct hit");
        // }
        // else if (collision.collider.CompareTag("Player"))
        // {
        //     GetComponent<collision>().TakeDamage(bulletType.explosionDmg);
        //     Debug.Log("enemy hi");
        // }
    }

    // private void OnParticleCollision(GameObject other)
    // {
    //     //count up collisions
    //     collisions++;
    //     Debug.Log(collisions);
    // }

    void explode()
    {
        if (bulletType.explosion != null)
        {
            Instantiate(bulletType.explosion, transform.position, Quaternion.identity);
        }

        // //check for enemies
        // Collider[] enemies = Physics.OverlapSphere(transform.position, bulletType.explosionRnge, bulletType.whatIsEnemies);
        Collider[] player = Physics.OverlapSphere(transform.position, bulletType.explosionRnge, bulletType.whatIsEnemies);
        for (int i = 0; i < player.Length; i++)
        {
            //Get component of enemy and call TakeDamage
        
            
            // enemies[i].GetComponent<enemyCollision>().TakeDamage(bulletType.explosionDmg);
            player[i].GetComponent<collision>().TakeDamage(bulletType.explosionDmg);
            //add explosion force to enemy (if enemy has a rigid body)
            // if(enemies[i].GetComponent<Rigidbody>())
            // {
            //     enemies[i].GetComponent<Rigidbody>().AddExplosionForce(bulletType.explosionFce, transform.position, bulletType.explosionRnge);
            // }
            
            if(player[i].GetComponent<Rigidbody>())
            {
                player[i].GetComponent<Rigidbody>().AddExplosionForce(bulletType.explosionFce, transform.position, bulletType.explosionRnge);
            }
        }

        // //checking to saee if works 
        // Invoke("Delay", 0.05f);
        Destroy(gameObject);
    }

    // void Delay()
    // {
    //     Destroy(gameObject);
    // }

    void Setup()
    {
        //create a new physic material
        phys_mat = new PhysicMaterial();
        phys_mat.bounciness = bulletType.bounciness;
        //to ensure the bullet bounces well 
        phys_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        phys_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        //assign material to collider
        GetComponent<SphereCollider>().material = phys_mat;

        //use grav
        bulletType.rb.useGravity = bulletType.usesGravity;

    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bulletType.explosionRnge);
    }
}
