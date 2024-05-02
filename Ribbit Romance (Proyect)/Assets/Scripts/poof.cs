using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poof : MonoBehaviour
{
    public GameObject bubble;
    private Rigidbody2D bubbleRB;
    private Vector2 initialForce;
    public float forceX;
    public float forceY;

    private void Awake()
    {
        bubbleRB = GetComponent<Rigidbody2D>();
        initialForce.x = Random.Range(-forceX, forceX);
        initialForce.y = forceY;

        bubbleRB.velocity = (initialForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Top"))
        {
            Destroy(bubble);
            Debug.Log("Mosca desintegrada");
        }
    }
}
