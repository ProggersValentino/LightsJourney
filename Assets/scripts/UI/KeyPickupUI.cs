using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;
    public GameObject keyImage;
    public inventory keys;
    void Start()
    {
        keys = GetComponent<inventory>();
    }

    // Update is called once per frame
   
}
