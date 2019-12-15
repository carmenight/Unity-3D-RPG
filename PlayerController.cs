using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
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
