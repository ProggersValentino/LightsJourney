using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class gunBehav : MonoBehaviour
{
    //accessing the data from the scriptable object
    public GStatsSO gunType;
    public LayerMask whatIsEnemy;
    
    //bools
    private bool shooting, RTS, reloading; //RTS = ready to shoot
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
    
    //dark
    private darknessHealth fogHP;
    
    //laser beam
    public ParticleSystem lightBeam;
    public ParticleSystem mainBeam;
    
    //light regen
    public int lightRegen;
    private void Awake() 
    {
        //ensuring mags are full
        bulletsLeft = gunType.magSize;
        RTS = true;
        // laserLine = GetComponent<LineRenderer>();
    }

    private void Update() 
    {
        if (isPlayer)
        {
            PInput();    
        }
        

        // //set ammo display, it exists
        // if(ammoDisplay != null)
        // {
        //     ammoDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magSize / bulletsPerTap);
        // }
    }

    void PInput()
    {
        //checking to see if player is allowed to hold  down the button
        if(gunType.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //reloading input
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < gunType.magSize && !reloading)
        {
            reload();
        }
        if(RTS && shooting && !reloading && bulletsLeft <= 0)
        {
            reload();
        }

        //shooting
        if(RTS && shooting && !reloading && bulletsLeft > 0)
        {
            //set bullets shot to 0
            bulletsShot = gunType.bulletsPerTap;

            if (gunType.projectileBased && !gunType.rayBased)
            {
                fire();
            }
            else if (!gunType.projectileBased && gunType.rayBased)
            {
                //laser beam
                lightBeam.Play(); //enables laser when player is firing
                mainBeam.Play();
                
                //raycast gun
                fireNonProj();
            }
            
        }
        else
        {
            mainBeam.Stop();
            lightBeam.Stop(); //disables laser when player stops pressing fire button
        }
        
        if (!Input.GetKey(KeyCode.Mouse0) && bulletsLeft < gunType.magSize)
        {
            StartCoroutine(regenLight());
            Debug.Log(bulletsLeft);
        }
        else
        {
            StopCoroutine(regenLight());
        }
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
        float x = Random.Range(-gunType.spread, gunType.spread);
        float y = Random.Range(-gunType.spread, gunType.spread);

        //calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //just dd spread spread to last

        //instantiate projectile
        GameObject currentBullet = Instantiate(gunType.bullet, ProjLaunchPoint.position, Quaternion.identity); //store instantiate projectile within GameObject

        //rotate bullet to face direction its been shot out keeping it consistent 
        currentBullet.transform.forward = directionWithSpread.normalized;
        
        //add forces to bullets
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * gunType.shootForce, ForceMode.Impulse); //forward direction of bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * gunType.upwardForce, ForceMode.Impulse); // to add upward force to any bullets (like grenade launchers)


        bulletsLeft--;
        bulletsShot++;

        //invoke resetShot function (if not already invoked), with your TbShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", gunType.TBShooting);
            allowInvoke = false;

            playerRb.AddForce(-directionWithSpread.normalized * gunType.recoilForce, ForceMode.Impulse); //adds recoil to gun. this has been placed so it only happens once every tap
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < gunType.bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("fire", gunType.TBShots);
        }

    }
    
    //shooting is done through raycasting 
    void fireNonProj()
    {
        RTS = false;
        // laserLine.enabled = true;
        
        
        float x = Random.Range(-gunType.spread, gunType.spread);
        float y = Random.Range(-gunType.spread, gunType.spread);
        
        //calculate new direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0f); //just dd spread spread to last
        
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, gunType.range,
                whatIsEnemy))
        {
            
            if (rayHit.collider.CompareTag("Enemy"))
            {
                Debug.Log(rayHit.collider.name);
                rayHit.collider.GetComponent<enemyCollision>().TakeDamage(gunType.dmg);
            }

            else if (rayHit.collider.CompareTag("Darkness"))
            {
                Debug.Log(rayHit.collider.name);
                rayHit.collider.GetComponent<darknessHealth>().TakeDamage(gunType.dmg);
                Debug.Log(rayHit.collider.GetComponent<darknessHealth>().currentHealth);
            }

            // draw the laser line between the camera and the hit point
            // laserLine.SetPosition(0, ProjLaunchPoint.position);
            // laserLine.SetPosition(1, rayHit.point);
        }
        else
        {
            // if the raycast did not hit anything, draw the laser line to the maximum range
            Vector3 endpoint = fpsCam.transform.position + (direction * gunType.range);
            // laserLine.SetPosition(0, ProjLaunchPoint.position);
            // laserLine.SetPosition(1, endpoint);
            
        }
        bulletsLeft--;
        Debug.Log(bulletsLeft);
        
        //invoke resetShot function (if not already invoked), with your TbShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", gunType.TBShooting);
            allowInvoke = false;

            playerRb.AddForce(-direction.normalized * gunType.recoilForce, ForceMode.Impulse); //adds recoil to gun. this has been placed so it only happens once every tap
        }
        
        
        // laserLine.enabled = false;
    }

    void ResetShot()
    {
        //Allow shooting and invoking again 
        RTS = true;
        allowInvoke = true;
    }

    void reload()
    {
        if (!usingLight)
        {
            reloading = true;
            Invoke("ReloadFinished", gunType.reloadT);    
        }
        
    }

    void ReloadFinished()
    {
        bulletsLeft = gunType.magSize;
        reloading = false;
    }
    
    private IEnumerator regenLight()
    {
        yield return new WaitForSeconds(2f); //slight delay before activating stamina regen
        while (bulletsLeft < gunType.magSize)
        {
            bulletsLeft += lightRegen;
            yield return new WaitForSeconds(1.5f); // wait for 1 second
        }
    }
}
