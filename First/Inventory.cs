using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
Inventory. Add and remove items. Takes stacking into account.
Bug(s) discovered: none yet
Additions:
textNotification - text that informs the player that they have acquired, discarded, or used an item
*/
public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory foud!");
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space = 20;
    public List<Item> items = new List<Item>();
    bool itemAlreadyInInventory;
    public TextMeshProUGUI textNotification;
    public Item teleport;
    public bool Add(Item item)
    {
    if (!item.isDefaultItem)
        {
            
            if (items.Count >= space)
            {
                Debug.Log("Not enough room in inventory.");
                return false;
            }
            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.name == item.name)
                {
                    inventoryItem.amount+= 1;
                    itemAlreadyInInventory = true;   
                }
            }
            if (!itemAlreadyInInventory)
            {
                items.Add(item);
                item.amount = 1;       
            }
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            StartCoroutine(Acquire(item));
        }
        return true;
    }
    public void Remove(Item item)
    {
        foreach (Item inventoryItem in items)
        {
            if (inventoryItem.name == item.name)
            {
                inventoryItem.amount = 0;
                itemAlreadyInInventory = false;
            }
        }
        if (!itemAlreadyInInventory)
        {
            items.Remove(item);
        }
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        if (ItemHandler.isDiscarded)
        {
            StartCoroutine(Discard(item));
        }
        else
        {
            StartCoroutine(Use(item));
        }
    }
    IEnumerator Acquire(Item item)
    {
        textNotification.text = "Acquired a " + item.name;
        yield return new WaitForSeconds(2);
        textNotification.text = "";
    }
    IEnumerator Discard(Item item)
    {
        textNotification.text = "Discarded a " + item.name;
        yield return new WaitForSeconds(2);
        textNotification.text = "";
    }
    IEnumerator Use(Item item)
    {
        textNotification.text = "Used a " + item.name;
        yield return new WaitForSeconds(2);
        textNotification.text = "";
    }
}
