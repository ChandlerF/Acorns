using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public int Price = 2;
    public int CollisionCount = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        CollisionCount++;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        CollisionCount--;
    }
}
