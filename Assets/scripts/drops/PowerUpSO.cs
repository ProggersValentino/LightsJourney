using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  PowerUpSO : ScriptableObject
{
    // template for creating powerups 
    public abstract void Apply(GameObject target);
    
    public abstract void RTOV(GameObject BgValue);
    
    public abstract float returnTime();
}
