using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public int Price = 2;
    public int CollisionCount = 0;

    private void OnCollisionEnter2D(Collision2D col)
    {
        CollisionCount++;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        CollisionCount--;
    }
}
