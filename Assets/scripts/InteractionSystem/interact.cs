using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interact : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactPRadius;
    [SerializeField] private LayerMask interactableMask;
    
    //keycode for interacting 
    public KeyCode interactKey;
    
    //events that will happen
    public UnityEvent interactEvents;
    
    //fpsCam
    public Camera fpsCam;
    public RaycastHit rayHit;
    public float viewRange;
    public LayerMask whatIsInteract;
    
    //inventory
    private inventory keyInventory; 
    
    public Collider[] colliders = new Collider[1];
    public int numFound;

    private void Start()
    {
        keyInventory = GetComponent<inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        
        // numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactPRadius, colliders, 
        // interactableMask);

        if (Physics.Raycast(ray, out rayHit, viewRange,
                whatIsInteract))
        {
            var interactable = rayHit.collider.GetComponent<IInteractable>(); //find any monobehav that is incorporating the Interface 
            
            //checks to see if player has keys 
            if (keyInventory.keys > 0)
            {
                //if the player in looking at an interactable and presses the interactable key then execute loop
                if (interactable != null && Input.GetKeyDown(interactKey))
                {
                    interactable.interact(this); //making the player the interactor
                    interactEvents.Invoke(); //activate unity event 
                    Debug.Log(rayHit.collider.name);
                }
            }
            else if(Input.GetKeyDown(interactKey))
            {
                interactable.interact(this);
            }
                
        }

        // if (numFound > 0)
        // {
        //     var interactable = colliders[0].GetComponent<IInteractable>(); //find nay monobehav that is incorporating the Interface 
        //
        //     if (interactable != null && Input.GetKeyDown(interactKey))
        //     {
        //         interactable.interact(this);
        //         Debug.Log(colliders[0]);
        //     }
        // }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactPRadius);
    }
}
