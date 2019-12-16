using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
Manages the slot UI in the inventory.
Bug(s) discovered: none yet
*/
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Item item;
    public int num;
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
