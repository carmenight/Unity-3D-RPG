using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
An example dialogue script. Might come back to this in the future.
Bug(s) discovered: none yet
Additions include:
continueButton - a UI button that runs NextSentence() on click
system - a gameobject which represents the player's guide. No particular meaning behind the name.
stats - a gameobject which represents the stat bar. Not activated until index is 12.
skip - text that tells the player they can press 'N' to skip the dialogue
item - item that will be added to the inventory at a certain index in the dialogue
*/
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public static int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject player;
    public static bool isMove = true;
    public GameObject system;
    public GameObject dialogue;
    public GameObject stats;
    public GameObject skip;
    public Item item;
    private bool hasDone;
    void Start()
    {
        StartCoroutine(Type());
    }
    void Update()
    {
        if (textDisplay.text == sentences[index] && index != 16 && index != 18)
        {
            continueButton.SetActive(true);
        }
        if (index == 17 && hasDone == false)
        {
            StartCoroutine(Type());
        }
        if (Input.GetKey(KeyCode.N) && dialogue.activeSelf)
        {
            index = 16;
            StartCoroutine(Type());
            stats.SetActive(true);
            
        }
        if (index > 12 && !stats.activeSelf)
        {
            stats.SetActive(true);
        }
    }
    IEnumerator Type()
    {
        if (index == 0)
        {
            system.SetActive(false);
        }
        if (index == 2)
        {
            system.SetActive(true);
            yield return new WaitForSeconds(2);
            var rotationVector = player.transform.rotation.eulerAngles;
            rotationVector.y = -40;
            player.transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (index == 12)
        {
            stats.SetActive(true);
        }
        if (index == 16)
        {
            Inventory.instance.Add(item);
            continueButton.SetActive(false);
            dialogue.SetActive(false);
            skip.SetActive(false);
        }
        if(index == 17)
        {
            dialogue.SetActive(true);
            isMove = false;
            hasDone = true;
        }
        if (index == 18)
        {
            isMove = true;
            dialogue.SetActive(false);
        }
        if (dialogue.activeSelf)
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentence()
    {
            continueButton.SetActive(false);
            if (index < sentences.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(Type());
            
            }
            else
            {
                textDisplay.text = "";
            }
    }  
}
