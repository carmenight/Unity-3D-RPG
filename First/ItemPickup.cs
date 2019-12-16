using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Acquire items by picking them up. Closely connected with script 'PlayerController'.
Bug(s) discovered: none yet
*/
public class ItemPickup : Interactables
{
    public Item item; 
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
