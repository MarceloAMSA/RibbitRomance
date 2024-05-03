using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public GameObject TryAgain;


    void Start()
    {
        TryAgain.SetActive(false);
        FindObjectOfType<AudioManager>().Mute("Clock");
        FindObjectOfType<AudioManager>().Stop("Clock");
        FindObjectOfType<AudioManager>().Play("Clock");
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
            TryAgain.SetActive(true);
            StartCoroutine(ReiniciarNivel(5));
        }
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    IEnumerator
        ReiniciarNivel(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        RestartLevel();


    }
}
 