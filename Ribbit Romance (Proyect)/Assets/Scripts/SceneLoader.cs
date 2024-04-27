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
    }

    void ChangeScene1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}