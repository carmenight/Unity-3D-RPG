using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/*
Manages inventory UI. Press 'I' to open and close panel.
Bug(s) discovered: none yet
*/
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;
    bool open = false;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            foreach (Item item in inventory.items)
        {
            item.amount = 0;
        }
        UpdateUI();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.I) && open)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            open = false;
        }
        else if (Input.GetKey(KeyCode.I) && !open)
        {
            inventoryUI.SetActive(inventoryUI.activeSelf);
            open = true;
        }
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
           
            }
            else
            {
                slots[i].ClearSlot();     
            }
        } 
    }
}

