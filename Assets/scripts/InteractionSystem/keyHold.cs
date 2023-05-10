using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyHold : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;

    public string interactionPrompt => _prompt;
    public string hasKey => _prompt2;
    
    //when activated it will execute the interface tings
    public bool interact(interact interacter)
    {
        var inventory = interacter.GetComponent<inventory>();

        if (inventory == null) return false;

        if (inventory.keys > 0) //checking if player has more than 0 keys 
        {
            Debug.Log("inserting Key");
            inventory.keys--;
            return true;
        }
        
        Debug.Log("key required");
        return false;
    }
}
