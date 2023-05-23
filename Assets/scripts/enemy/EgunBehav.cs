using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EgunBehav : MonoBehaviour
{
    //accessing the data from the scriptable object
    
    public GStatsSO Gun;
    public LayerMask whatIsEnemy;
    
    //bools
    private bool shooting, shootingSecond, RTS, reloading; //RTS = ready to shoot
    public bool isPlayer;
    public bool usingLight;

    public int bulletsLeft, bulletsShot;
    
    //references
    public Camera fpsCam;
    public RaycastHit rayHit;
    public Transform ProjLaunchPoint;
    
    //references
    public Rigidbody playerRb;
    
    //fixing of bugs 
    public bool allowInvoke = true;
    

    //light regen
    public int lightRegen;
    private void Awake() 
    {
        //ensuring mags are full
        bulletsLeft = Gun.magSize;
        RTS = true;
    }

    //shooting done through projectiles
    public void fire()
    {
        RTS = false;
        //Find the exact hit position using raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); // this is just a point far away from the player
        }

        //calculate the direction from launch point 
        Vector3 directionWithoutSpread = targetPoint - ProjLaunchPoint.position;

        //calculate spread
        float x = Random.Range(-Gun.spread, Gun.spread);
        float y = Random.Range(-Gun.spread, Gun.spread);

        //calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //just dd spread spread to last

        //instantiate projectile
        GameObject currentBullet = Instantiate(Gun.bullet, ProjLaunchPoint.position, Quaternion.identity); //store instantiate projectile within GameObject

        //rotate bullet to face direction its been shot out keeping it consistent 
        currentBullet.transform.forward = directionWithSpread.normalized;
        
        //add forces to bullets
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * Gun.shootForce, ForceMode.Impulse); //forward direction of bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * Gun.upwardForce, ForceMode.Impulse); // to add upward force to any bullets (like grenade launchers)

    }
    
   
    void ResetShot()
    {
        //Allow shooting and invoking again 
        RTS = true;
        allowInvoke = true;
    }
    
    
    
   
}
