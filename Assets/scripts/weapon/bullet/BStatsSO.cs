using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "bullet type", menuName = "ScriptableObjects/New bullet")]
public class BStatsSO : ScriptableObject
{
    //assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies; //detects whether it is an enemy 

    //stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool usesGravity;

    //damage
    public int directDmg;
    public float explosionDmg;
    public float explosionRnge;
    public float explosionFce;

    //LifeTime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;
    

}
