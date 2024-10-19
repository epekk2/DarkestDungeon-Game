using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectableObject : MonoBehaviour
{
    public string itemName;   // Name of the item
    public Sprite itemIcon;   // Optional icon for the item
    public int itemQuantity = 1; // Amount of object to pick up

    private InventoryClass playerInventory;

    void Start()
    {
        // Find the Inventory in the scene (assuming there's one Inventory component in the game)
        playerInventory = FindObjectOfType<InventoryClass>();
    }

    void OnMouseDown()
    {
        if (playerInventory != null && playerInventory.itemList.Count < 5)
        {
            // Create a new item and add it to the player's inventory
            Item newItem = new Item(itemName, itemIcon);
            playerInventory.AddItem(newItem, itemQuantity);

            // Destroy the object after collecting
            Destroy(gameObject);
        }
    }
}
