using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed; // Player movement speed

    public InventoryClass playerInventory;

    void Awake()
    {
        speed = 5;
    }
    void Update()
    {
        // Detect if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Check for a clicked object using a raycast
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //UnityEngine.Debug.Log("here");
                // If the clicked object has an ItemPickup component
                //public GameObject linked;
               /* Item item = hit.collider.GetComponent<Item>();
                
                if (item != null)
                {
                    // Add the item to the player's inventory
                    playerInventory.insertItem(item);

                    // Destroy the item in the game world
                    Destroy(hit.collider.gameObject);

                   // Debug.Log("Picked up " + itemPickup.itemData.itemName);
                }
               */
            }
        }

        if (Input.GetKey(KeyCode.W))
        { // Controls movements in all four directions
            transform.Translate(Vector2.up * testVert(Vector2.up));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * testHor(Vector2.left));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * testVert(Vector2.down));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * testHor(Vector2.right));
        }
    }

    private float testHor(Vector2 dir)
    {
        Vector2 origin = transform.position;
        float offset;
        if (dir.Equals(Vector2.left))
        {
            offset = -0.125f; // Offset for left side of character
        }
        else
        {
            offset = 0.125f; // Offset for right side of character
        }
        origin.x += offset;
       
        return speed * Time.deltaTime;
    }

    private float testVert(Vector2 dir)
    {
        Vector2 origin = transform.position;
        float offset;
        if (dir.Equals(Vector2.up))
        {
            offset = .1f; // Offset for top side of character
        }
        else
        {
            offset = -0.5f; // Offset for bottom side of character
        }
        origin.y += offset;

        return speed * Time.deltaTime;
    }
}