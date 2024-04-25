using System.Collections;
using UnityEngine;

public class moscasbye : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject moscaPrefab;
    public float spawnDelay = 2f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            StartCoroutine(ByeMosca());
        }
    }

    IEnumerator ByeMosca()
    {
        yield return new WaitForSeconds(spawnDelay / 2f);
        Instantiate(moscaPrefab, player.position, Quaternion.identity);
    }
}