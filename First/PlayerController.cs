using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
/*
Player movement on a flat terrain. Move using the arrow keys or WASD. I am working on gravity, which I have commented out.
Bug(s) discovered: issues with rotation after teleportation(this stems from the CameraController script, but it would be a good idea to look at this one)
Note that there are some additions besides simple movement, including:
Item.isTeleport - bool value taken from the item script, which tells whether or not to change player position and rotation. In this case, the new position is set to the enemy's x, current y, and the enemy's z+40.
Dialogue.isMove - bool value taken from the dialogue script, which will disable the player's movement when set to false.
anim.SetFloat - changes the value of float "Speed" to the player's movement, which is 0 when not moving. In this case, when the float "Speed"  is greater than 0, the animation will transition from idle to walking.
isDialogue - is set to true. When the player's position exceeds the boundaries of the terrain, it will activate the dialogue and the system, set "Speed" to 0, and rotate the player towards system. Only occurs once. I might change this part in the future. 
system - a gameobject which represents the player's guide. No particular meaning behind the name.
SetFocus - interaction with an interactable
pivot - a gameobject which represents an empty gameobject that stores the camera's rotation
*/
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;
    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;
    public Camera cam;
    public Interactables focus;
    public GameObject system;
    public GameObject dialogue;
    public GameObject enemy;
    public bool isDialogue = true;
    public GameObject player;
    public TextMeshProUGUI dialogueText;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (Item.isTeleport)
        {
            player.transform.position = new Vector3(enemy.transform.position.x, player.transform.position.y, enemy.transform.position.z + 40);
            Item.isTeleport = false;
            playerModel.transform.LookAt(enemy.transform);
            EnemyAttack.isStart = true;
        }
        if (Dialogue.isMove)
        {
            float yStore = moveDirection.y;
            moveDirection = Camera.main.transform.TransformDirection(moveDirection);
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            if (moveDirection != Vector3.zero)
            {
                RemoveFocus();
            }
            moveDirection.y = yStore;
            //moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                controller.Move(moveDirection * Time.deltaTime);
            }
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
            anim.SetFloat("Speed", ((Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")))));
            if (Input.GetKey(KeyCode.E))
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Interactables interactable = hit.collider.GetComponent<Interactables>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
            }
        }
        if (playerModel.transform.position.x <= -10 || playerModel.transform.position.y <= -10 || playerModel.transform.position.z <= -10 || playerModel.transform.position.x >= 1010 || playerModel.transform.position.y >= 1010 || playerModel.transform.position.z >= 1010)
        {  
            if (isDialogue)
            {
                system.transform.position = new Vector3(playerModel.transform.position.x - 10, playerModel.transform.position.y, playerModel.transform.position.z + 5);
                system.SetActive(true);
                playerModel.transform.LookAt(system.transform);
                Dialogue.index = 17;
                dialogueText.text = "";
                dialogue.SetActive(true);
                isDialogue = false;
                anim.SetFloat("Speed", 0);
            }
        }
    }

    void SetFocus(Interactables newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
                focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null; 
    }
}
