using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Player attack. Not an average attack system with swords and magic. Uses a line that connects to the enemy. Line disappears when player distance from the enemy is greater than 50.
Bug(s) discovered: none yet
*/
public class PlayerAttack : MonoBehaviour
{
    public LineRenderer lr;
    private float counter;
    private float dist;
    public Transform origin;
    public Transform destination;
    public float lineDrawSpeed = 2f;
    public bool isAttacking;
    public GameObject beam;
    public GameObject enemy;
    public GameObject player;
    void Update()
    {
        dist = Vector3.Distance(origin.position, destination.position);
        if (Input.GetKey(KeyCode.Alpha1))
        {
            isAttacking = true;
            lr.enabled = true;
        }
        if (dist > 50)
        {
            isAttacking = false;
            lr.enabled = false;
            counter = 0;
            beam.SetActive(false);
        }
        if (isAttacking)
        {
            lr.SetPosition(0, origin.position);
            beam.transform.position = origin.position;
            beam.SetActive(true);
        }
        if (counter < dist && isAttacking)
        {
            dist = Vector3.Distance(origin.position, destination.position);
            counter += .1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;
            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            lr.SetPosition(1, pointAlongLine);
        }
        if (isAttacking)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(0.2f);
            player.GetComponent<PlayerStats>().AddHealth(0.2f);
        }
    }
}
