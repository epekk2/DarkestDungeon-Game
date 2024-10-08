using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryClass : MonoBehaviour
{
    public List<InventorySlot> itemList = new List<InventorySlot>();  // The list of items in the inventory

    // Reference to the inventory panel (UI)
    public Transform inventoryPanel;

    // Reference to the inventory slot prefab
    public GameObject inventorySlot;


    // Function to add an item to the inventory
    public void AddItem(Item newItem, int quantity = 1)
    {
        // Check if the item already exists in the inventory
        InventorySlot existingSlot = itemList.Find(slot => slot.item.itemName == newItem.itemName);

        if (existingSlot != null)
        {
            // If the item exists, increase its quantity
            existingSlot.AddQuantity(quantity);
            UnityEngine.Debug.Log("Added " + quantity + " more of: " + newItem.itemName + ". New total: " + existingSlot.quantity);
        }
        else
        {
            // If the item doesn't exist, add a new slot with the item
            InventorySlot newSlot = new InventorySlot(newItem, quantity);
            itemList.Add(newSlot);

            // Add the item to the UI panel
            AddItemToUI(newItem);

            UnityEngine.Debug.Log("Added new item: " + newItem.itemName + " with quantity: " + quantity);
        }
    }

    // Function to add item sprite to the UI
    public void AddItemToUI(Item newItem)
    {
        // Instantiate a new inventory slot (UI Image) in the inventory panel
        GameObject newSlotUI = Instantiate(inventorySlot, inventoryPanel);

        // Set the item's sprite in the UI slot
        Image itemImage = newSlotUI.GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = newItem.itemIcon;
        }
    }



}
