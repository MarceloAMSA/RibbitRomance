using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button Startbutton;
    public Button Quitbutton;

    void Start()
    {
        Startbutton.onClick.AddListener(ChangeScene1);
        Quitbutton.onClick.AddListener(QuitGame);
        FindObjectOfType<AudioManager>().Play("Main Menu Theme");
    }

    void ChangeScene1()
    {
        FindObjectOfType<AudioManager>().Stop("Main Menu Theme");
        SceneManager.LoadScene("Carta");
        
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

}