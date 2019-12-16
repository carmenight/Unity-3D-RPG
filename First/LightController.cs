using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Script for a light that follows the enemy.
Bug(s) discovered: none yet
*/
public class LightController : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
    }
}
