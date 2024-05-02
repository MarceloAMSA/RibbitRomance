using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MoscasNivel2 : MonoBehaviour
{
    public GameObject Cave;
    public Transform player;

    public static bool damageCheck;
    public static Vector3 damageSource;
    private float dmgTimer = 1.5f;

    public GameObject moscaPrefab;
    public float spawnDelay;

    private Animator anim;
    private int moscasnivel1 = moscas.moscasTotales;

    [SerializeField] private TMP_Text contador;
    [SerializeField] private TMP_Text MoscaText;

    private void Start()
    {
        anim = GetComponent<Animator>();
        MoscaText.enabled = false;
        contador.text = moscasnivel1 + " / 8";
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
        MoscaText.enabled = false;

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
                    FindObjectOfType<AudioManager>().Play("BubbleSpawn");
                }
            }
            if (moscasnivel1 == 3)
            {
                MoscaText.enabled = true;
                MoscaText.text = "oh no tengo muy pocas moscas :(";
                StartCoroutine(QuitarTexto(3));

            }

            if (collision.CompareTag("Cave") && moscasnivel1 >= 4)
            {
                SceneManager.LoadScene("EndingGood"); ;
            }

            else if (collision.CompareTag("Cave") && moscasnivel1 < 4)
            {
                SceneManager.LoadScene("EndingBad"); ;
            }
        }


    }
    IEnumerator
        QuitarTexto(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        MoscaText.enabled = false;
    }

    IEnumerator ByeMosca()
    {
        yield return new WaitForSeconds(spawnDelay / 2f);
        Instantiate(moscaPrefab, player.position, Quaternion.identity);
    }

}