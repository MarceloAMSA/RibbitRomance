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

    //Variables del c�digo
    bool isMouseDown;
    Vector3 lastVelocity;
    public static Vector3 camOffset;
    public static bool SwimUpCheck;
    public static bool SwimDownCheck;


    //Variables para ajustar controles en Unity
    public float force;
    public float minimoSaltoY;
    public float maximoSaltoY;
    public float minimoSaltoX;
    public float maximoSaltoX;
    public float maximoLinea;

    void Start()
    {
        //Asignar componentes a variables, dos vertices en la l�nea
        frogRB = GetComponent<Rigidbody2D>();
        frogSprite = GetComponent<SpriteRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        //Variable para calcular rebotes
        lastVelocity = frogRB.velocity;

        SwimUpCheck = false;
        SwimDownCheck = false;



        //Asignar vertice #1 de la l�nea
        lineRenderer.SetPosition(0, frogRB.position);

        //Cambiar orientaci�n de la rana
        if (frogRB.velocity.x < -0.2)
        {
            frogSprite.flipX = true;
        }
        else if (frogRB.velocity.x > 0.2)
        {
            frogSprite.flipX = false;
        }

        //Crea la l�nea de trayectoria
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

    //Resetea la l�nea al centro
    void ResetLine()
    {
        SetLine(frogRB.position);
    }

    //Crea posici�n del v�rtice #2, obtiene offset de c�mara
    void SetLine(Vector3 position)
    {
        Vector2 direction = (frogRB.position - (Vector2)position).normalized;
        float vectorDistance = Vector3.Magnitude(Vector3.ClampMagnitude(((Vector2)position - frogRB.position), maximoLinea));
        Vector2 lineEnd = (frogRB.position + direction * vectorDistance); // Puedes ajustar el valor "vectorDistance" para cambiar la longitud de la l�nea     
        lineRenderer.SetPosition(1, lineEnd);
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
            
            if (frogRB.velocity.y >= 0.1f)
            {
                SwimUpCheck = true;
            }
            else if (frogRB.velocity.y < -0.1f)
            {
                SwimDownCheck = true;
            }
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
        Vector3 frogForce = (position - frogRB.position) * force * -1;
        frogForce = limitesSalto(frogForce);
        frogRB.velocity = frogForce;

    }

    //L�mites de fuerza de salto
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