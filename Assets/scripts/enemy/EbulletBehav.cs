using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EbulletBehav : MonoBehaviour
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
    void FixedUpdate() 
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
    
    void explode()
    {
        if (bulletType.explosion != null)
        {
            Instantiate(bulletType.explosion, transform.position, Quaternion.identity);
        }
        
        // Debug.Log(bulletType.explosion);
        // //check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, bulletType.explosionRnge, bulletType.whatIsEnemies);
        
        for (int i = 0; i < enemies.Length; i++)
        {
            //Get component of player health script and call TakeDamage
            enemies[i].GetComponent<collision>().TakeDamage(bulletType.explosionDmg); 
            
            
            //add explosion force to enemy (if enemy has a rigid body)
            // if(enemies[i].GetComponent<Rigidbody>())
            // {
            //     enemies[i].GetComponent<Rigidbody>().AddExplosionForce(bulletType.explosionFce, transform.position, bulletType.explosionRnge);
            // }
            
            // if(enemies[i].GetComponent<Rigidbody>())
            // {
            //     enemies[i].GetComponent<Rigidbody>().AddExplosionForce(bulletType.explosionFce, transform.position, bulletType.explosionRnge);
            // }
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision) 
    {
        //count up collisions
        collisions++;
    
        // //explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Player") && bulletType.explodeOnTouch)
        {
            Debug.Log("explode");
            explode();
        }
        if (collision.collider.CompareTag("Ground") && bulletType.explodeOnTouch)
        {
            Debug.Log("groynded");
            explode();
        }
    }

    // private void OnParticleCollision(GameObject other)
    // {
    //     //count up collisions
    //     collisions++;
    //     Debug.Log(collisions);
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
