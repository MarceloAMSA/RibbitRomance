using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;


public class MoscasNivel1 : MonoBehaviour
{

    private Rigidbody2D flyHitbox;

    private int countRamo = 0;
    private int countText = 0;

    [SerializeField] private TMP_Text EndText;
    [SerializeField] private TMP_Text contador;
    [SerializeField] private TMP_Text MoscaText;
    public Image dialogueBox;
    public Image dialogueSprite;
    public Animator transition;

    private void Start()
    {
        MoscaText.enabled = false;
        EndText.enabled = false;
        dialogueBox.enabled = false;
        dialogueSprite.enabled = false;
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

            if (moscas.moscasTotales == 4)
            {
                dialogueBox.enabled = true;
                dialogueSprite.enabled = true;
                MoscaText.enabled = true;
                MoscaText.text = "Con esto será suficiente," + Environment.NewLine + "tal vez...";
                StartCoroutine(QuitarTexto(4));
            }

            if (moscas.moscasTotales == 8)
            {
                dialogueBox.enabled = true;
                dialogueSprite.enabled = true;
                MoscaText.enabled = true;
                MoscaText.text = "Ahora sí, que no digan" + Environment.NewLine + "que no resuelvo.";
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
            dialogueBox.enabled = true;
            dialogueSprite.enabled = true;
            MoscaText.text = "¡Este ramo seguro le saca" + Environment.NewLine + "una sonrisa!";
            StartCoroutine(QuitarTexto(4));
        }


        else if (collision.CompareTag("Pond"))
        {
            if (countRamo == 1 && moscas.moscasTotales >= 4)
            {
                StartCoroutine(SceneTransition(1));         
            }

            else if (countRamo == 0 && moscas.moscasTotales >= 4)
            {
                MoscaText.enabled = true;
                dialogueBox.enabled = true;
                dialogueSprite.enabled = true;
                MoscaText.text = "Creo que ese ramo le gustaría mucho...";
                StartCoroutine(QuitarTexto(5));
            }

            else if (countRamo == 1 && moscas.moscasTotales < 4)
            {
                MoscaText.enabled = true;
                dialogueBox.enabled = true;
                dialogueSprite.enabled = true;
                MoscaText.text = "¡Necesito más moscas antes de irme!";
                StartCoroutine(QuitarTexto(5));
            }

            else if (countRamo == 0 && moscas.moscasTotales < 4)
            {
                MoscaText.enabled = true;
                dialogueBox.enabled = true;
                dialogueSprite.enabled = true;
                MoscaText.text = "¡Necesito más moscas antes de irme!";
                StartCoroutine(QuitarTexto(5));
            }
        }


    }

    IEnumerator
    QuitarTexto(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        dialogueBox.enabled = false;
        dialogueSprite.enabled = false;
        EndText.enabled = false;
        MoscaText.enabled = false;
    }

    IEnumerator
    SceneTransition(int segundos)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(segundos);
        FindObjectOfType<AudioManager>().Stop("Lvl1 Theme");
        SceneManager.LoadScene("BetweenCutscene");

    }
}
