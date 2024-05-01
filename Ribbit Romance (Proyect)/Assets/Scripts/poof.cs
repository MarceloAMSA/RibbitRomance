using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poof : MonoBehaviour
{
    public GameObject bubble;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Top"))
        {
            Destroy(bubble);
        }
    }
}
