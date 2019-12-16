using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Example item manager. Adds items and monitors their usage.
Bug(s) discovered: none yet
Additions include:
currentHealth - current health of player
shieldsRecieved - amount of shields received
shieldUse - int set to 1 when using shield, and set to 0 when not using shield
damageBlocked - float that stores how much damage the shield has blocked
healsRecievedOne, healsRecievedTwo, healsRecievedThree - int set to 1 when using heal, and set to 0 when not using heal
*/
public class ItemTracker : MonoBehaviour
{
    public int shieldsRecieved;
    public Item shield;
    public GameObject shieldObject;
    public GameObject player;
    public Item heal;
    public static float damageBlocked;
    public int healsRecievedOne;
    public int healsRecievedTwo;
    public int healsRecievedThree;
    public static bool combatStart;
    public bool isFirst = true;
    void Update()
    {
        if (PlayerStats.currentHealth < 50)
        {
            combatStart = true;
        }
        if (PlayerStats.currentHealth <= 20 && shieldsRecieved == 0)
        {
            Inventory.instance.Add(shield);
            shieldsRecieved += 1;
        }
        if (PlayerStats.currentHealth >= 40)
        {
            shieldsRecieved = 0;
        }
        if (Item.shieldUse == 1)
        {
            shieldObject.SetActive(true);
            shieldObject.transform.position = player.transform.position;
        }
        if (damageBlocked >= 20f)
        {
            shieldObject.SetActive(false);
            Item.shieldUse = 0;
            damageBlocked = 0;
        }
        if (PlayerStats.currentHealth <= 30)
        {
            isFirst = false;
            healsRecievedOne = 0;
        }
        if (PlayerStats.currentHealth >= 40 && PlayerStats.currentHealth < 60 && healsRecievedOne != 1 && combatStart)
        {
            if (!isFirst)
            {
                Inventory.instance.Add(heal);
                healsRecievedOne = 1;
            }
        }
        if (PlayerStats.currentHealth >= 60 && PlayerStats.currentHealth < 80 && healsRecievedTwo != 1 && combatStart)
        {
            Inventory.instance.Add(heal);
            healsRecievedTwo = 1;
        }
        if (PlayerStats.currentHealth >= 80 && PlayerStats.currentHealth < 100 && healsRecievedThree != 1 && combatStart)
        {
            Inventory.instance.Add(heal);
            healsRecievedThree = 1;
        }
    }
}
