using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerManager : MonoBehaviour
{
    public List<timeItem> managingTime;
    
    private void Update()
    {

        if (managingTime != null)
        {
            for (int i = managingTime.Count - 1; i >= 0; i--)
            {
                timeItem regen = managingTime[i]; //accesses the particular element with
                if (Time.time > regen.timeToTurn)
                {
                    regen.regening.TriggerFog();
                    managingTime.Remove(regen);
                }
            }    
        }
        
    }
}


[Serializable]
public class timeItem
{
    public darknessHealth regening;
    
    public float timeToTurn;
    /// <summary>
    /// constructer for adding items to list
    /// </summary>
    /// <param name="fog"> the gameobject that fogs your screen</param>
    /// <param name="time"></param>
    public timeItem(darknessHealth fog, float time)
    {
        regening = fog;
        timeToTurn = time;
    }
}
