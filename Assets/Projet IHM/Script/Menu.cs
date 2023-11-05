using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        
    }

    public void Play()
    {
        FindObjectOfType<AudioManager>().Stop("lose");
        FindObjectOfType<AudioManager>().Stop("death");
        FindObjectOfType<AudioManager>().Stop("crowd");
        FindObjectOfType<AudioManager>().Stop("fireworks");
        FindObjectOfType<AudioManager>().Play("main");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
