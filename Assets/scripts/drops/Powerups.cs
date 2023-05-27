using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Powerups : MonoBehaviour
{
    public PowerUpSO PUEffect;

    // powerup activation
    private void OnTriggerEnter(Collider collision) {
        
        if(collision.CompareTag("Player"))
        {
            temporaryBuff buff = Instantiate(PUEffect) as temporaryBuff; //allow access to the stateselection otherwise it wont work 
            
            Debug.Log("im detected");
            if(buff.stateSelection == temporaryBuff.buffState.temporary) StartCoroutine(PUActivation(buff,collision)); //activating coroutine
            if(buff.stateSelection == temporaryBuff.buffState.permanent)
            {
                Debug.Log("perma doje");
                itemAdd(buff, collision); //permanently add the item
            }
            
                

            Debug.Log(buff.stateSelection);    
            // StartCoroutine(PUActivation(collision));
        }
        
        
        
    }

    // temporary buff activation through a coroutine
    public IEnumerator PUActivation(temporaryBuff buff, Collider collision)
    {
        PUEffect.Apply(collision.gameObject); //changed value
        
        GetComponent<MeshRenderer>().enabled = false;

        Debug.Log("start duration");
        yield return new WaitForSeconds(PUEffect.returnTime()); // the duration for changed value

        Debug.Log("co donbe");
        PUEffect.RTOV(collision.gameObject); // original value

        Destroy(gameObject);
    }

    public void itemAdd(temporaryBuff buff, Collider collision)
    {
        PUEffect.Apply(collision.gameObject);
        Destroy(gameObject);
    }



   
}
