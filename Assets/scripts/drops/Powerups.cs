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
            Debug.Log("im detected");
            // if(temporaryBuff.stateSelection == temporaryBuff.buffState.temporary) StartCoroutine(PUActivation(collision)); //activating coroutine
            // if(temporaryBuff.stateSelection == temporaryBuff.buffState.permanent) 
            // itemAdd(collision); //permanently add the item
            StartCoroutine(PUActivation(collision));
        }
        
        Debug.Log(temporaryBuff.stateSelection);
        
    }

    // temporary buff activation through a coroutine
    public IEnumerator PUActivation(Collider collision)
    {
        PUEffect.Apply(collision.gameObject); //changed value
        
        GetComponent<MeshRenderer>().enabled = false;

        Debug.Log("start duration");
        yield return new WaitForSeconds(PUEffect.returnTime()); // the duration for changed value

        Debug.Log("co donbe");
        PUEffect.RTOV(collision.gameObject); // original value

        Destroy(gameObject);
    }

    public void itemAdd(Collider collision)
    {
        PUEffect.Apply(collision.gameObject);
        Destroy(gameObject);
    }



   
}
