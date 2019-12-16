using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Manages interactions. Closely connected with script 'PlayerController'. Uses virtual void Interact(), which is meant to be overwritten in an item script.
Bug(s) discovered: none yet
*/
public class Interactables : MonoBehaviour
{
    public float radius = 5f;
    bool isFocus = false;
    public Transform player;
    bool hasInteracted = false;
    public virtual void Interact()
    {
        //overwrite
        Debug.Log("Interacting with " + transform.name); 
    }
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            Interact();
            hasInteracted = true;
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
    } 
}
