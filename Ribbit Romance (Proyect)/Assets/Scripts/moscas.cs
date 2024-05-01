using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class moscas : MonoBehaviour
{
    public GameObject Pond;
    public GameObject Ramo;

    public GameObject Cave;

    private int countRamo = 0;

    private int mosca = 0;
    private Animator anim;
    public GameObject mosquita;
    private int Enough = 1;

    [SerializeField] private TMP_Text EndText;
    [SerializeField] private TMP_Text contador;
    [SerializeField] private TMP_Text MoscaText;

    private void Start()
    {
        anim = GetComponent<Animator>();
        MoscaText.enabled = false;
        EndText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoscaText.enabled = false;
        EndText.enabled = false;

        if (collision.gameObject.CompareTag("moscas"))
        {
            mosca++;
            Debug.Log(mosca);
            Destroy(collision.gameObject);

            contador.text = mosca + " / 8";
        }

        if (collision.CompareTag("Trap") & mosca > 0)
        {
            mosca--;
            Debug.Log(mosca);
            contador.text = mosca + " / 8";
            Enough = 0;
          
        }

        if (mosca == 4 & Enough == 1)
        {
            Enough = 2;
            MoscaText.enabled = true;
            MoscaText.text = "Con esto deber?a ser suficiente";
            StartCoroutine(QuitarTexto(3));
        }
        if (mosca == 3 & Enough == 0)
        {
            MoscaText.enabled = true;
            MoscaText.text = "oh no tengo muy pocas moscas :(";
            StartCoroutine(QuitarTexto(3));

        }

        if (collision.CompareTag("Ramo"))
        {
            countRamo++;
            Debug.Log(countRamo);
            Destroy(Ramo);
        }

        if (collision.CompareTag("Pond") && countRamo == 1 && mosca >= 4)
        {
            SceneManager.LoadScene("BetweenCutscene"); ;
        }

        else if (collision.CompareTag("Pond") && countRamo == 0 && mosca >= 4)
        {
            EndText.enabled = true;
            EndText.text = "¡Un ramo es el regalo perfecto!";
            StartCoroutine(QuitarTexto(5));
        }
        else if (collision.CompareTag("Pond") && countRamo == 1 && mosca < 4)
        {
            EndText.enabled = true;
            EndText.text = "Necesito más moscas";
            StartCoroutine(QuitarTexto(5));
        }

        if (collision.CompareTag("Cave") && mosca >= 4)
        {
            SceneManager.LoadScene("EndingGood"); ;
        }

        else if (collision.CompareTag("Cave") && mosca < 4)
        {
            SceneManager.LoadScene("EndingBad"); ;
        }



    }
    IEnumerator
        QuitarTexto(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        MoscaText.enabled = false;
    }
}