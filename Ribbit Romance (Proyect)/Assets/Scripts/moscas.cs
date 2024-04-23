using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class moscas : MonoBehaviour
{
    private int mosca = 0;
    private Animator anim;
    public GameObject mosquita;
    private int Enough = 1;

    [SerializeField] private TMP_Text contador;
    [SerializeField] private TMP_Text MoscaText;

    private void Start()
    {
        anim = GetComponent<Animator>();
        MoscaText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoscaText.enabled = false;

        if (collision.gameObject.CompareTag("moscas"))
        {
            mosca++;
            Debug.Log(mosca);

            //anim.SetTrigger("collected");
            Destroy(collision.gameObject);

            contador.text = mosca + " / 8";
        }

        if (collision.CompareTag("Trap"))
        {
            mosca--;
            Debug.Log(mosca);
            contador.text = mosca + " / 8";
            Enough = 0;
           // anim.SetTrigger("ouch");
        }

        if (mosca == 4 & Enough == 1)
        {
            Enough = 2;
            MoscaText.enabled = true;
            MoscaText.text = "Con esto debería ser suficiente";
            StartCoroutine(QuitarTexto(3));
        }
        if (mosca == 3 & Enough == 0)
        {
            MoscaText.enabled = true;
            MoscaText.text = "oh no tengo muy pocas moscas :(";
            StartCoroutine(QuitarTexto(3));

        }
    }
    IEnumerator
        QuitarTexto(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        MoscaText.enabled = false;
    }
}