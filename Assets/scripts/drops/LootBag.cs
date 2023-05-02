using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject PUGen;
    public List<temporaryBuff> lootList = new List<temporaryBuff>();

    temporaryBuff getDroppedPU()
    {
        float ranNum = Random.Range(1f, 101f); // going from 1 - 100 
        List<temporaryBuff> possiblePUs = new List<temporaryBuff>();

        foreach (temporaryBuff PU in lootList)
        {
            if (ranNum <= PU.dropChance)
            {
                possiblePUs.Add(PU); //
            }

            if (possiblePUs.Count > 0) //checking to see if there are any power ups to drop based on drop chance
            {
                temporaryBuff droppedPU = possiblePUs[Random.Range(0, possiblePUs.Count)]; //if there is a possibility then it will choose 
                // Debug.Log(PUEffect);
                return droppedPU;
            }
            
        }
        return null;
    }
    
    //spawning the loot
    public void spawnPU(Vector3 spawnPos)
    {
        temporaryBuff droppedPU = getDroppedPU();
        if (droppedPU != null)
        {

            GameObject puGameObject = Instantiate(PUGen, spawnPos, Quaternion.Euler(90, 0, 0)); //spawns the powerup
            puGameObject.GetComponent<MeshFilter>().mesh = droppedPU.PUModel;
            Powerups power = puGameObject.GetComponent<Powerups>();
            power.PUEffect = droppedPU;
            Debug.Log(power.PUEffect);
        }
    }

    
}
