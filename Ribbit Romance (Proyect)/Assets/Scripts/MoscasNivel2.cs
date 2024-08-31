using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class MoscasNivel2 : MonoBehaviour
{
    public GameObject Cave;
    public Transform player;

    public static bool damageCheck;
    public static Vector3 damageSource;
    private float dmgTimer = 1.5f;
    private Collider2D frogCollider;

    public GameObject moscaPrefab;
    public float spawnDelay;

    public Animator transition;
    public Image dialogueBox;
    public Image dialogueSprite;
    private int moscasnivel1;

    [SerializeField] private TMP_Text contador;
    [SerializeField] private TMP_Text MoscaText;

    private void Start()
    {
        frogCollider = GetComponent<Collider2D>();
        moscasnivel1 = moscas.moscasTotales;
        contador.text = moscasnivel1 + " / 8";
        MoscaText.enabled = false;
        dialogueBox.enabled = false;
        dialogueSprite.enabled = false;

        StartCoroutine(Advertencia(4));


    }

    private void Update()
    {
        if (dmgTimer > 0)
        {
            dmgTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Trap"))
        {
            if (dmgTimer <= 0)
            {
                dmgTimer = 1.5f;
                damageCheck = true;
                damageSource = collision.transform.position;

                if (moscasnivel1 > 0)
                {
                    moscasnivel1--;
                    Debug.Log(moscasnivel1);
                    contador.text = moscasnivel1 + " / 8";
                    StartCoroutine(ByeMosca());

                    if (moscasnivel1 == 0)
                    {
                        FindObjectOfType<AudioManager>().Play("FlyEmpty");
                        dialogueBox.enabled = true;
                        dialogueSprite.enabled = true;
                        MoscaText.enabled = true;
                        MoscaText.text = "Ya valió.";
                        StartCoroutine(QuitarTexto(5));
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("BubbleSpawn");
                    }


                }

            }
        }
    

        if (collision.CompareTag("Cave") && moscasnivel1 >= 1)
            {
                StartCoroutine(SceneTransitionGood(1));    
            }

        else if (collision.CompareTag("Cave") && moscasnivel1 < 1)
            {
                StartCoroutine(SceneTransitionBad(1));
            }
        }


    
    IEnumerator
        QuitarTexto(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        MoscaText.enabled = false;
        dialogueBox.enabled = false;
        dialogueSprite.enabled = false;

    }

    IEnumerator ByeMosca()
    {
        yield return new WaitForSeconds(spawnDelay / 2f);
        Instantiate(moscaPrefab, player.position, Quaternion.identity);
    }
    IEnumerator SceneTransitionGood(int segundos)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(segundos);
        FindObjectOfType<AudioManager>().Stop("Lvl2 Theme");
        FindObjectOfType<AudioManager>().Stop("Clock");
        SceneManager.LoadScene("EndingGood");

    }
    IEnumerator SceneTransitionBad(int segundos)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(segundos);
        FindObjectOfType<AudioManager>().Stop("Lvl2 Theme");
        FindObjectOfType<AudioManager>().Stop("Clock");
        SceneManager.LoadScene("EndingBad");

    }
    IEnumerator Advertencia(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        dialogueBox.enabled = true;
        dialogueSprite.enabled = true;
        MoscaText.enabled = true;
        MoscaText.text = "Debo tener cuidado o podría" + Environment.NewLine + "perder la cena.";
        StartCoroutine(QuitarTexto(5));
    }
}