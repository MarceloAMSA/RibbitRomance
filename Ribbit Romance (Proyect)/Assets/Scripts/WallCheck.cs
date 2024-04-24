using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public static bool wallCollision;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        wallCollision = true;
    }

     private void OnTriggerExit2D(Collider2D collision)
    {
        wallCollision = false;
    }
    
}
