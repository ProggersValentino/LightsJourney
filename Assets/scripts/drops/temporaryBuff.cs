using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PowerUps/Temporary")]
public class temporaryBuff : PowerUpSO
{
    [Header("Basic Item Data")]
    public Mesh PUModel;
    public string PUName;
    public float dropChance;
    
    
    //type of variation
    public enum buffType {health, fireRate, ammunition}
    public buffType buffSelection;

    //if its perma or temporary
    public enum buffState {temporary, permanent}
    public static buffState stateSelection;
    public buffState stating = stateSelection;
    
    
    [Header("variation amounts")]
    public int amount;
    public float OGV;
    [SerializeField] float timeLength;
    
    
    public void Loot(string PUName, float dropChance)
    {
        this.PUName = PUName;
        this.dropChance = dropChance;
    }

    public override void Apply(GameObject target)
    {
        if (buffSelection == buffType.ammunition)
        {
            target.GetComponentInChildren<gunBehav>().bulletsLeft += amount;
        }
        else if (buffSelection == buffType.health)
        {
            // target.GetComponent<Health>().health = amount;
        }
        
       
    }

    public override void RTOV(GameObject BgValue)
    {
        if (buffSelection == buffType.fireRate)
        {
         // BgValue.GetComponentInChildren<turretControl>().fireRate = OGV;
        }
        else if (buffSelection == buffType.health)
        {
            // BgValue.GetComponent<Health>().health = OGV;
        }
    }

    // returns the time length
    public override float returnTime()
    {
        return timeLength;
    }
    

    
}
