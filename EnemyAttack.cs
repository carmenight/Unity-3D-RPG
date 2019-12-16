using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
An example enemy attack script. Very limited, requires a specific gameobject setup. I'm working on general improvements.
How it works:
Four gameobjects under one parent, parent under enemy. Script attached to parent. Assign the parent to 'wave', and the children to the other ones. 
The parent has (0, 0, 0) rotation, (0, y-value, 0) position.
'waveOne' has (0, 0, 0) rotation, (0, 0, 0) position.
'waveTwo' has (0, 0, 0) rotation, (0, 0, z-value) position.
'waveThree' has (0, 0, 0) rotation, (0, 0, z-value+constant) position.
'waveFour' has (0, 0, 0) rotation, (0, 0, z-value+2(constant)) position.
Enemy looks at player every six seconds. The gameobjects are activated one by one, appearing in front of the enemy and towards the player.
When collision is detected or player does not move away, player takes damage and enemy adds the same amount of health (in this case, enemy and player share a health bar).
If shield has been used, the player does not take damage and the amount is added to ItemTracker.damageBlocked.
Bug(s) discovered: script is attached to parent, and collider as well. Collision is detected before children gameobjects collide with player.
Additions:
shield - item that blocks damage 
ItemTracker.damageBlocked - float that stores how much damage the shield has blocked
Item.shieldUse - int set to 1 when using shield, and set to 2 when not using shield
*/
public class EnemyAttack : MonoBehaviour
{
    public GameObject wave;
    public GameObject waveOne;
    public GameObject waveTwo;
    public GameObject waveThree;
    public GameObject waveFour;
    public GameObject enemy;
    public GameObject player;
    public static bool isStart;
    public GameObject playerScript;
    void Start()
    {
        waveOne.SetActive(false);
        waveTwo.SetActive(false);
        waveThree.SetActive(false);
        waveFour.SetActive(false);
    }
    void Update()
    {
        if (isStart)
        {
            StartCoroutine(Attack());
            isStart = false;
        }
    }
    IEnumerator Attack()
    {
        enemy.transform.LookAt(player.transform);
        waveOne.SetActive(true);
        yield return new WaitForSeconds(1);
        waveTwo.SetActive(true);
        yield return new WaitForSeconds(1);
        waveThree.SetActive(true);
        yield return new WaitForSeconds(1);
        waveFour.SetActive(true);
        yield return new WaitForSeconds(3);
        waveOne.SetActive(false);
        waveTwo.SetActive(false);
        waveThree.SetActive(false);
        waveFour.SetActive(false);
        StartCoroutine(Attack());
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "PlayerIdle")
        {
            if (Item.shieldUse == 1)
            {
                ItemTracker.damageBlocked += 5f;
            }
            else
            {
                playerScript.GetComponent<PlayerStats>().TakeDamage(5f);
                enemy.GetComponent<EnemyStats>().AddHealth(5f);
            }
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "PlayerIdle")
        {
            if (Item.shieldUse == 1)
            {
                ItemTracker.damageBlocked += 0.1f;
            }
            else
            {
                playerScript.GetComponent<PlayerStats>().TakeDamage(0.1f);
                enemy.GetComponent<EnemyStats>().AddHealth(0.1f);
            }
        }
    }
}
