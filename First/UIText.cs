using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
UI text for stacks in the inventory. 
Bug(s) discovered: none yet
*/
public class UIText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject inventory;
    public Inventory list;
    public int slotNum;
    public int itemNum = 1;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        list = inventory.GetComponent<Inventory>();
    }
    void Update()
    {
        itemNum = 1;
        foreach (Item item in list.items)
        {
            if (item.amount > 1 && slotNum == itemNum)
            {
                text.text = item.amount.ToString();
            }
            else if (item.amount <= 1 && slotNum == itemNum)
            {
                text.text = "";
            }
            itemNum++;
        }
        if (list.items.Count == 0)
        {
            text.text = "";
        }
    }
}
