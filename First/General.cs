using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
Game management script.
Includes:
game pause and unpause 
game over and game won
transitions between scenes
exit game
Bug(s) found: none yet
*/
public class General : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject fadeIn;
    public static bool GameOver;
    public static bool GameWon;
    public GameObject text;
    public GameObject textTwo;
    public GameObject textWon;
    public bool isDead;
    public GameObject instructions;
    public GameObject exit;
    void Update()
    {
        if (IntroText.start)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        
        if (IntroText.textNum == 8)
        {
            fadeIn.SetActive(true);
            IntroText.textNum = 9;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (GameOver)
        {
            SceneManager.LoadScene(3);
        }
        if (GameWon)
        {
            SceneManager.LoadScene(3);
        }
    }
    void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(2);
    }
    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    void Start()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Exit")
        {
            StartCoroutine(FadeIn());
        }
        GameOver = false;
        GameWon = false;
    }
    IEnumerator FadeIn()
    {
        if (PlayerStats.currentHealth <= 0)
        {
            isDead = true;
        }
        yield return new WaitForSeconds(2);
        if (isDead)
        {
            text.SetActive(true);
        }
        else
        {
            textWon.SetActive(true);
        }
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(5);
        textTwo.SetActive(true);
    }
    public void Instructions()
    {
        instructions.SetActive(true);
        exit.SetActive(true);
    }
    public void Exit()
    {
        instructions.SetActive(false);
        exit.SetActive(false);
    }

}   

