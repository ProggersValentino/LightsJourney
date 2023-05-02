using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatewaySys : MonoBehaviour
{
    public bool k1Found, k2Found, k3Found;
    public MeshRenderer[] keys;

    public GameObject doorMesh;
    private BoxCollider portal;
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
        unlockingDoor();
        doorHandler();
    }

    void doorHandler()
    {
        if (k1Found && k2Found && k3Found)
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

    public void unlockingDoor()
    {
        if (k1Found)
        {
            keys[0].enabled = true;

            // Debug.Log("colour changed");
        }

        if (k2Found)
        {
            keys[1].enabled = true;
        }
        
        if (k3Found)
        {
            keys[2].enabled = true;
        }
    }

    void resetKCol()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].enabled = false;
        }
    }

    void changeDoorM()
    {
        
    }
    
}
