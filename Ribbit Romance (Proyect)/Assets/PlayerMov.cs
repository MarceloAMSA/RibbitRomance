using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayerMov : MonoBehaviour
{
    public GameObject frog;
    public LineRenderer lineRenderer;
    public Transform linePosition;
    public Transform center;
    public float baseForce;


    public Vector3 currentPosition;

    bool isMouseDown;

    Rigidbody2D frogRB;
    Collider2D frogCollider;

    public float force;

    // Start is called before the first frame update
    void Start()
    {
        frogRB = GetComponent<Rigidbody2D>();
        frogCollider = GetComponent<Collider2D>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, frogRB.position);

        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            SetLine(mousePosition);
        }
        else
        {
            ResetLine();
        }
    }

    void ResetLine()
    {
        SetLine(frogRB.position);
    }

    void SetLine(Vector3 position)
    {       
                 
        lineRenderer.SetPosition(1, position);

    }

    private void OnMouseUp()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        isMouseDown = false;
        Shoot(mousePosition);
    }

    private void OnMouseDown()
    {       
        isMouseDown = true;        
    }

    void Shoot(Vector3 position)
    {
        Vector3 frogForce = (position - center.position) * force * -1;      
        frogRB.velocity = frogForce;        
    }
    
}
