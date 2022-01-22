using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource gameSound;

    void Start()
    {
        //Getting the sound sources
		gameSound = GetComponent<AudioSource>();
        
        // play scoreSound sound
        gameSound.Play();

    }
    public void PlayGame ()
    {
        SceneManager.LoadScene("minigame");
    }

    public void ExitGame ()
    {
        Debug.Log("Exited!");
        Application.Quit();
    }
}
