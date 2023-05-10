using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GatewaySys : MonoBehaviour
{
    public bool k1Found, k2Found, k3Found;
    public GameObject[] keys;
    public GameObject[] keyGuidance;

    public GameObject doorMesh;
    private BoxCollider portal;

    public interact rayInformation;

    public enum doorState
    {
        locked,
        unlocked
    }

    public doorState currentDoor;

    private void Start()
    {
        resetKCol();
        doorMesh.SetActive(false);
        portal = GetComponent<BoxCollider>();
        portal.enabled = false;
    }

    private void Update()
    {
        // unlockingDoor();
        doorHandler();
    }

    void doorHandler()
    {
        int unlockedAlt = 0;
        
        //loop to go through all the alters and check to see if they are active
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].activeSelf)
            {
                unlockedAlt++;
            }
        }

        //if all the alters are unlocked then activate the portal 
        if (unlockedAlt == keys.Length)
        {
            currentDoor = doorState.unlocked;
            doorMesh.SetActive(true);
            portal.enabled = true;

        }
        else
        {
            currentDoor = doorState.locked;
        }
    }

    public void findKeys()
    {
        if (rayInformation.rayHit.collider.name == "KeyAlter_L")
        {
           accessingParticles(keyGuidance[0]);
           Debug.Log(keyGuidance[0].name);
        }

        if (rayInformation.rayHit.collider.name == "KeyAlter_R")
        { 
            accessingParticles(keyGuidance[1]);
            Debug.Log(keyGuidance[1].name);
        }

        if (rayInformation.rayHit.collider.name == "KeyAlter_Mid")
        {
            accessingParticles(keyGuidance[2]);
            Debug.Log(keyGuidance[2 ].name);
        }
        
    }
    
    //goes through a activates all the particle systems within the gameobject container
    public void accessingParticles(GameObject containers)
    {
        ParticleSystem[] partSys = containers.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem part in partSys)
        {
            part.Play();
        }
    }
    
    
    public void unlockingDoor()
    {
        if (rayInformation.rayHit.collider.name == "KeyAlter_L" )
        {
            Debug.Log("keyinserted");
            keys[0].SetActive(true);
            // Debug.Log("colour changed");
        }

        if (rayInformation.rayHit.collider.name == "KeyAlter_R")
        {
            // keys[1].enabled = true;
            keys[1].SetActive(true);
        }
        
        if (rayInformation.rayHit.collider.name == "KeyAlter_Mid")
        {
            // keys[2].enabled = true;
            keys[2].SetActive(true);
        }
    }

    void resetKCol()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            // keys[i].enabled = false;
            keys[i].SetActive(false);
        }
    }

}
