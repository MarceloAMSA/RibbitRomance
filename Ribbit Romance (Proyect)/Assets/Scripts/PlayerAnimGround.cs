using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimGround : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D frogRB;
    public static bool groundCollision;

    //Ennumeración de condiciones para animaciones
    private enum MovementState {idle, jumping, falling, landing};
    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        frogRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        state = MovementState.landing;

    }

    private void UpdateAnimation()
    {
        // Condiciones del salto

        if (frogRB.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (frogRB.velocity.y < -0.1f) //agregar el menos
        {
            state = MovementState.falling;
        }
        else if (frogRB.velocity.y == 0f)
        {
            state = MovementState.landing;
        }
            

        anim.SetInteger("state", (int)state);
    }



    

}
