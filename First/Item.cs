using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
Example item script. Originally, I was planning on making a class for each item that derived from 'Item', and then override Use(). But I later decided to condense everything into one script.
Bug(s) discovered: none yet
Note that there are some additions, including:
isTeleport - whether to teleport the player or not
shieldUse - whether the shield is in effect or not
currentHealth - the current health of the player or the enemy
"Teleport Item", "Shield Item", "Health Item" - various items
*/
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Item item;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int amount;
    public static bool isTeleport;
    public static int shieldUse;
    public virtual void Use()
    {
        if (name == "Teleport Item")
        {
            isTeleport = true;
            if (item.amount == 1)
            {
                Inventory.instance.Remove(item);
            }
            else
            {
                item.amount -= 1;
            }
        }
        if (name == "Shield Item" && shieldUse == 0)
        {
            if (item.amount == 1)
            {
                Inventory.instance.Remove(item);
            }
            else
            {
                item.amount -= 1;
            }
            shieldUse = 1;
        }
        if (name == "Health Item")
        {
            if (item.amount == 1)
            {
                Inventory.instance.Remove(item);
            }
            else
            {
                item.amount -= 1;
            }
            PlayerStats.currentHealth += 10f;
            EnemyStats.currentHealth -= 10f;
        }
    }
}
