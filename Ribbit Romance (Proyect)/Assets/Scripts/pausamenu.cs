using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class pausamenu : MonoBehaviour
{
    public Button Pausa;
    public GameObject PopMenu;
    public Button Continuar;
    public Button Menu;
    public Button Reiniciar;
    private string Scene;


    void Start()
    {
        PopMenu.SetActive(false);
        Pausa.onClick.AddListener(Pause);

        Continuar.onClick.AddListener(Play);

        Menu.onClick.AddListener(ChangetoMenu);

        Reiniciar.onClick.AddListener(RestartLevel);

    }

    void Pause()
    {
        PopMenu.SetActive(true);
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().Play("Pause");
    }

    void Play()
    {
        PopMenu.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("Pause");
    }

    void ChangetoMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("Exit");
        SceneManager.LoadScene("StartMenu");
        moscas.moscasTotales = 0;
        FindObjectOfType<AudioManager>().Stop("Lvl1 Theme");
        FindObjectOfType<AudioManager>().Stop("Lvl2 Theme");
        FindObjectOfType<AudioManager>().Stop("Clock");
        FindObjectOfType<AudioManager>().Play("Main Menu Theme");
    }

    private void RestartLevel()
    {
        PopMenu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Restart");
        Scene = SceneManager.GetActiveScene().name;
        if (Scene == "Level1")
        {
            FindObjectOfType<AudioManager>().Stop("Lvl1 Theme");
            SceneManager.LoadScene("Level1");
            moscas.moscasTotales = 0;
            Time.timeScale = 1f;
            FindObjectOfType<AudioManager>().Play("Lvl1 Theme");
        }
        if (Scene == "Level2")
        {
            FindObjectOfType<AudioManager>().Stop("Lvl2 Theme");
            FindObjectOfType<AudioManager>().Stop("Clock");
            SceneManager.LoadScene("Level2");
            Time.timeScale = 1f;         
            FindObjectOfType<AudioManager>().Play("Lvl2 Theme");
        }

    }

}
