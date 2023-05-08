using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyHold : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string interactionPrompt => _prompt;
    
    //when activated it will execute the interface tings
    public bool interact(interact interacter)
    {
        var inventory = interacter.GetComponent<inventory>();

        if (inventory == null) return false;

        if (inventory.keys > 0)
        {
            Debug.Log("inserting Key");
            inventory.keys--;
            return true;
        }
        
        Debug.Log("key required");
        return false;
    }
}
