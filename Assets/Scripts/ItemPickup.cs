using System.Diagnostics;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;  // Reference to the item this pickup represents
    private InventoryClass inventory;
    private bool isBeingTouched = false;

    void Start()
    {
        // Find the inventory component
        inventory = FindObjectOfType<InventoryClass>();
        if (inventory == null)
        {
            //Debug.LogError("No InventoryClass found in scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isBeingTouched)
        {
            isBeingTouched = true;
            inventory.currentTouchingItem = item;
            //Debug.Log("Player touching " + item.itemName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isBeingTouched)
        {
            isBeingTouched = false;
            inventory.currentTouchingItem = null;
           // Debug.Log("Player stopped touching " + item.itemName);
        }
    }
}