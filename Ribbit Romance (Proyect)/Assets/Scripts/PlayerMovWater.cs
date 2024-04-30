using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayerMovWater : MonoBehaviour
{
    //Para obtener y asignar componentes de la rana
    public LineRenderer lineRenderer;
    Rigidbody2D frogRB;
    SpriteRenderer frogSprite;
    Animator anim;

    //Variables del código
    bool isMouseDown;
    Vector3 lastVelocity;
    public static Vector3 camOffset;
    public static bool SwimCheck;


    //Variables para ajustar controles en Unity
    public float force;
    public float minimoSaltoY;
    public float maximoSaltoY;
    public float minimoSaltoX;
    public float maximoSaltoX;
    public float maximoLinea;

    void Start()
    {
        //Asignar componentes a variables, dos vertices en la línea
        frogRB = GetComponent<Rigidbody2D>();
        frogSprite = GetComponent<SpriteRenderer>();
        lineRenderer.positionCount = 4;
    }

    void Update()
    {
        //Variable para calcular rebotes
        lastVelocity = frogRB.velocity;
    




        //Asignar vertice #1 de la línea
        lineRenderer.SetPosition(0, frogRB.position);

        //Cambiar orientación de la rana
        if (frogRB.velocity.x < -0.2)
        {
            frogSprite.flipX = true;
    
        }
        else if (frogRB.velocity.x > 0.2)
        {
            frogSprite.flipX = false;
        }

        //Crea la línea de trayectoria
        if (isMouseDown)
        {       
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);           
            SetLine(mousePosition);           
        }
        else
        {
            ResetLine();
        }
    }

    //Resetea la línea al centro
    void ResetLine()
    {
        SetLine(frogRB.position);
    }

    //Crea posición del vértice #2, obtiene offset de cámara
    void SetLine(Vector3 position)
    {
        Vector2 direction = (frogRB.position - (Vector2)position).normalized;
        float vectorDistance = Vector3.Magnitude(Vector3.ClampMagnitude(((Vector2)position - frogRB.position), maximoLinea));

        Vector2 lineEnd1 = (frogRB.position + direction * (vectorDistance * 0.899f)); // Puedes ajustar el valor "vectorDistance" para cambiar la longitud de la línea     
        Vector2 lineEnd2 = (frogRB.position + direction * vectorDistance * 0.9f);
        Vector2 lineEnd3 = (frogRB.position + direction * (vectorDistance * 1.2f));
        lineRenderer.SetPosition(1, lineEnd1);
        lineRenderer.SetPosition(2, lineEnd2);
        lineRenderer.SetPosition(3, lineEnd3);
        camOffset = direction * vectorDistance * 0.5f;
    }

    //Activa el disparo cuando se libera el mouse
    private void OnMouseUp()
    {
        
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);          
            Shoot(mousePosition);
            SwimCheck = true;

            FindObjectOfType<AudioManager>().Play("Swim");


        }
        isMouseDown = false;

    }

    //Activa variable cuando se presiona el mouse
    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    //Mecaniso de disparo
    void Shoot(Vector2 position)
    {
        Vector2 frogForce = (position - frogRB.position) * force * -1;
        frogForce = limitesSalto(frogForce);
        frogRB.velocity = frogRB.velocity + frogForce;
        
    }


    //Límites de fuerza de salto
    Vector3 limitesSalto(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, minimoSaltoY, maximoSaltoY);
        vector.x = Mathf.Clamp(vector.x, minimoSaltoX, maximoSaltoX);

        return vector;
    }

    //Calcula rebotes cuando se colisiona con paredes o techo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (WallCheck.wallCollision)
        {
            var speed = lastVelocity.magnitude;
            var dir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            frogRB.velocity = dir * speed*0.5f;

            
        }
        
    }
   
}