using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour

{
    public int keys;
    //Key UI
   
    public int keysFound;

    // Key UI
    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;

    public void UpdateUI()
    {
        Key1.SetActive(keysFound >= 1);
        Key2.SetActive(keysFound >= 2);
        Key3.SetActive(keysFound >= 3);
    }

    // Collision script Calls this method when a key is found in the on trigger method
    public void KeyFound()
    {
        keysFound++;
        UpdateUI();
    }
}

