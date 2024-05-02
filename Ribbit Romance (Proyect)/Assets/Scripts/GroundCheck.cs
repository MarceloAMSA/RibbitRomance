using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool isGrounded;
    private float failsafeTimer = 5f;

    private void Update()
    {
        if (failsafeTimer > 0)
            {
                failsafeTimer -= Time.deltaTime;
            }
        if (failsafeTimer <= 0f)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
        failsafeTimer = 5f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }


}
