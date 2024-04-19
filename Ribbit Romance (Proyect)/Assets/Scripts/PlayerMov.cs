using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayerMov : MonoBehaviour
{
    //Para poder asignar componentes de la rana y la trayectoria
    public GameObject frog;
    public LineRenderer lineRenderer;
    public Transform linePosition;
    public Transform center;
    public float baseForce;


    public Vector2 currentPosition;

    bool isMouseDown;

    //Para obtener componentes de la rana
    Rigidbody2D frogRB;
    Collider2D frogCollider;
    SpriteRenderer frogSprite;

    //Variables para ajustar controles en Unity
    public float force;
    public float minimoSalto;
    public float maximoSalto;
    public float maximoLínea;
    Vector3 mouseClick;

    void Start()
    {
        //Asignar componentes a variables
        frogRB = GetComponent<Rigidbody2D>();
        frogCollider = GetComponent<Collider2D>();
        frogSprite = GetComponent<SpriteRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        //Cambiar orientación de la rana
        if (frogRB.velocity.x < -0.2)
        {
            frogSprite.flipX = false;
        }
        else if (frogRB.velocity.x > 0.2)
        {
            frogSprite.flipX = true;
        }



        lineRenderer.SetPosition(0, frogRB.position);

        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = frogRB.position + Vector2.ClampMagnitude(currentPosition - frogRB.position, maximoLínea);
            SetLine(currentPosition);
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
        mouseClick = Input.mousePosition;
        isMouseDown = true;
        
    }

    void Shoot(Vector3 position)
    {
        Vector3 frogForce = (position - center.position) * force * -1;

        frogForce = limitesSalto(frogForce);
        frogRB.velocity = frogForce;
    }

    Vector3 limitesSalto(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, minimoSalto, maximoSalto);
        vector.x = Mathf.Clamp(vector.x, minimoSalto, maximoSalto);
        return vector;       
    }
}
