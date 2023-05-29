using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private GatewaySys keyfinder;

    private void Start()
    {
        keyfinder = FindObjectOfType<GatewaySys>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keyfinder.nearestObj != null) transform.LookAt(keyfinder.nearestObj.transform.position);
    }


}
