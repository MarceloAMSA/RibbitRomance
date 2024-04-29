using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAnimWater : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D frogRB;
    public static bool groundCollision;


    //Ennumeración de condiciones para animaciones
    private enum MovementState { idle, swimUp, swimDown, damage };
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
        PlayerMovWater.SwimCheck = false;


    }

    private void UpdateAnimation()
    {
        // Condiciones del salto

        if (PlayerMovWater.SwimCheck == false)
        {
            state = MovementState.idle;
        }
        else
            if (frogRB.velocity.y > -0.2f)
            {
                state = MovementState.swimUp;
            }
            else
            {
                state = MovementState.swimDown;
            }
        {
        }

        anim.SetInteger("state", (int)state);
    }



    

}
