using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
   public string interactionPrompt { get; } //prompt to display 

   public bool interact(interact interacter);
}
