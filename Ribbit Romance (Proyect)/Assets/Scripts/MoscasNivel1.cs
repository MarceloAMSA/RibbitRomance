using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;


public class MoscasNivel1 : MonoBehaviour
{

    private Rigidbody2D flyHitbox;

    private int countRamo = 0;
    private int countText = 0;

    [SerializeField] private TMP_Text EndText;
    [SerializeField] private TMP_Text contador;
    [SerializeField] private TMP_Text MoscaText;

    private void Start()
    {
        MoscaText.enabled = false;
        EndText.enabled = false;
        contador.text = moscas.moscasTotales + " / 8";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("moscas"))
        {
            Destroy(collision.gameObject);

            moscas.moscasTotales += 1;


            if (moscas.moscasTotales == 8)
            {
                FindObjectOfType<AudioManager>().Play("FlyComplete");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("FlyCollect");
            }

            if (moscas.moscasTotales == 4 & countText == 0)
            {
                MoscaText.enabled = true;
                MoscaText.text = "Con esto debería ser suficiente";
                StartCoroutine(QuitarTexto(4));
            }

            contador.text = moscas.moscasTotales + " / 8";

            Debug.Log(moscas.moscasTotales);
        }

      
        else if (collision.CompareTag("Ramo"))
        {
            Destroy(collision.gameObject);
            countRamo = 1;
            Debug.Log(countRamo);
            MoscaText.enabled = true;
            MoscaText.text = "¡Un ramo es el regalo perfecto!";
            StartCoroutine(QuitarTexto(4));
        }


        else if (collision.CompareTag("Pond"))
        {
            if (countRamo == 1 && moscas.moscasTotales >= 4)
            {
                FindObjectOfType<AudioManager>().Stop("Lvl1 Theme");
                SceneManager.LoadScene("BetweenCutscene");
            }

            else if (countRamo == 0 && moscas.moscasTotales >= 4)
            {
                EndText.enabled = true;
                EndText.text = "¡Un ramo es el regalo perfecto!";
                StartCoroutine(QuitarTexto(5));
            }

            else if (countRamo == 1 && moscas.moscasTotales < 4)
            {
                EndText.enabled = true;
                EndText.text = "Necesito más moscas";
                StartCoroutine(QuitarTexto(5));
            }

            else if (countRamo == 0 && moscas.moscasTotales < 4)
            {
                EndText.enabled = true;
                EndText.text = "Necesito más moscas";
                StartCoroutine(QuitarTexto(5));
            }
        }


    }

    IEnumerator
    QuitarTexto(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        MoscaText.enabled = false;
        countText = 1;
    }
}
