using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostBuilding : Buildings
{
    public bool CanPlace = false;

    void Update()
    {
        Vector3 spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        transform.position = spawnPos;


        if(base.CollisionCount > 0)
        {
            CannotPlaceBuilding();
        }
        else CanPlaceBuilding();
    }


    
    public void Initialize(GameObject selectedBuilding)
    {
        Vector3 spawnPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        transform.position = spawnPos;


        SpriteRenderer sr = transform.AddComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite  = selectedBuilding.GetComponent<SpriteRenderer>().sprite;
        //Need to set sorting layer


        BoxCollider2D collider = transform.AddComponent<BoxCollider2D>();
        collider.isTrigger= true;

        Rigidbody2D rb = transform.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        transform.localScale = selectedBuilding.transform.localScale;
    }


    private void CannotPlaceBuilding()
    {
        CanPlace = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void CanPlaceBuilding()
    {
        CanPlace = true;
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
