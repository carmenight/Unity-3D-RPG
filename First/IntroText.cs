using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/*
Very simplistic text script meant for introduction scenes. I use this when I don't want to make an actual introduction scene.
Bug(s) discovered: none yet
Additions:
anim.Play = plays an animation from the animator component
*/
public class IntroText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animator anim;
    public static int textNum = 1;
    public static bool start;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
        textNum = 1;
    }
    void Update()
    {
        if (anim.IsInTransition(0))
        {
            textNum += 1;
        }
        if (textNum == 1)
        {
            text.text = "Something.";
            anim.Play("IntroTextFade");
        }
        else if (textNum == 2)
        {
            text.text = "Something else.";
            anim.Play("IntroTextFade");
        }
        else if (textNum == 3)
        {
            start = true;
            SceneManager.LoadScene(2);
        }
    }
}
   
