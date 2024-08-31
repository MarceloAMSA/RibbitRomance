using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public Animator transition;


    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Clock");
        FindObjectOfType<AudioManager>().Mute("Clock");
        remainingTime = 90;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        if (remainingTime < 31)
        {
            timerText.color = Color.red;

        }

        if (remainingTime < 11)
        {
            FindObjectOfType<AudioManager>().Unmute("Clock");
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime == 0)
        {
            FindObjectOfType<AudioManager>().Stop("Clock");
            
            StartCoroutine(ReiniciarNivel(1));
        }
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<AudioManager>().Play("Lvl2 Theme");
    }


    IEnumerator
        ReiniciarNivel(int segundos)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(segundos);
        FindObjectOfType<AudioManager>().Stop("Lvl2 Theme");
        RestartLevel();
    }
}
 