using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFlipped : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    public float decrease;
    private float previousSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointA.transform;
        previousSpeed = speed;
        flip();
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint.position.x == pointA.transform.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 5f
            && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
            speed = previousSpeed;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 5f
            && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
            speed = previousSpeed;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 15f)
        {
            speed = decrease;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}