using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimWater : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D frogRB;
    public static bool groundCollision;
    public float swimSpeedUp;
    public float swimSpeedDown;

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
    }

    private void UpdateAnimation()
    {
        // Condiciones del salto

        if (frogRB.velocity.y > swimSpeedDown && frogRB.velocity.y < swimSpeedUp)
        {
            state = MovementState.idle;
        }

        else if (PlayerMovWater.SwimUpCheck == true)
        {
            state = MovementState.swimUp;
        }
        else if (PlayerMovWater.SwimDownCheck == true)
        {
            state = MovementState.swimDown;
        }

    


        anim.SetInteger("state", (int)state);
    }



    

}
