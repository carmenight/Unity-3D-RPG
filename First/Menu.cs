using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
New game. 
Bug(s) discovered: none yet
*/
public class Menu : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject loadText;
    public void NewGame()
    {
        StartCoroutine(NewGameStart());
    }
    IEnumerator NewGameStart()
    {
        if (IntroText.start)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
        else
        {
            fadeOut.SetActive(true);
            yield return new WaitForSeconds(3);
            loadText.SetActive(true);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(1);
        }
    }
}
