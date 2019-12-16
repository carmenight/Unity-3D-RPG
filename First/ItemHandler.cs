using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
Player discards an item by dragging it out of the inventory interface. Returns to the inventory if the distance isn't large enough.
Bug(s) discovered: none yet
*/
public class ItemHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    bool isDrop;
    public int slotNum;
    public int itemNum = 1;
    public GameObject inventory;
    public Inventory list;
    public Vector3 position;
    public static bool isDiscarded;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        transform.SetAsLastSibling();
        CameraController.isDragging = true;
        itemNum = 1;
        foreach (Item itemI in list.items)
        {
            if (slotNum == itemNum)
            {
                item = itemI;
            }
            itemNum++;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.position.x >= 300 && transform.position.x <= 1460 && transform.position.y - position.y <= 150)
        {
            transform.position = position;
        }
        CameraController.isDragging = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
      if (transform.position.x >= 300 && transform.position.x <= 1460 && transform.position.y - position.y <= 150)
        {
            isDrop = false;
        }
        else
        {
            isDrop = true;
        }
        if (isDrop)
        {
            isDiscarded = true;
            Inventory.instance.Remove(item);
            isDiscarded = false;
        }
    }
    void Start()
    {
        list = inventory.GetComponent<Inventory>();
        position = transform.position;
    }
}
    
