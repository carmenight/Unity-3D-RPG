using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
Plays music.
Bug(s) discovered: none yet
Additions:
Item.isTeleport - whether or not to teleport the player
*/
public class Music : MonoBehaviour
{
    public AudioSource sad;
    public AudioSource epic;
    void Start()
    {
        sad.Play();
        epic.mute = true;
    }
    void Update()
    {
        if (Item.isTeleport)
        {
            epic.mute = false;
            sad.Pause();
        }
    }
}
