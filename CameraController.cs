using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
Camera movement. Follows the player, and rotates by dragging. I'm testing out better ways to rotate. 
Bug(s) discovered: issues with rotation after teleportation
Note that there are some additions besides simple movement, including:
Item.isTeleport - bool value taken from the item script, which tells whether or not to change camera rotation. Still working on this.
pivot - a gameobject which represents an empty gameobject that stores the camera's rotation.
*/
public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;
    public static bool isDragging;
    public GameObject enemy;
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 10f);
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;

        pivot.transform.parent = null;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Dialogue.isMove)
        {
            if (Item.isTeleport)
            {
                pivot.Rotate(0, -180 + transform.rotation.y, 0);
            }
            transform.position = target.position - (Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y, 0) * offset);
            if (transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
            }
            //if (EventSystem.current.IsPointerOverGameObject())
                //return;
            transform.LookAt(target);
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                if (!isDragging)
                {
                    pivot.transform.position = target.transform.position;
                    float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
                    pivot.Rotate(0, horizontal, 0);
                    float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
                    pivot.Rotate(-vertical, 0, 0);
                    float desiredYAngle = pivot.eulerAngles.y;
                    float desiredXAngle = pivot.eulerAngles.x;
                    Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
                }

            }
            if (Item.isTeleport)
            {
                pivot.Rotate(0, -180 + transform.rotation.y, 0);
            }
        }   
    }
}
