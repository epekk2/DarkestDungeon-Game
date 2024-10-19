using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InventoryClass : MonoBehaviour
{
    public List<InventorySlot> itemList = new List<InventorySlot>();  // The list of items in the inventory

    public Transform inventoryPanel; // Reference to the inventory panel (UI)

    public GameObject inventorySlot; // Reference to the inventory slot prefab

    public int maxUniqueItems = 5;  // variable to control max size of inventory

    public int selectedItemIndex = -1; // index of item currently selected in list

    public GameObject selectedItemUI;  // UI element of the currently selected item

    void Update()
    {
        if (selectedItemIndex >= 0 && Input.GetKeyDown(KeyCode.E))
        {
            UseSelectedItem();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CycleToNextItem();
        }
    }

    // Function to add an item to the inventory
    public void AddItem(Item newItem, int quantity = 1)
    {
        // Check if inventory is already full
        if (itemList.Count >= maxUniqueItems)
        {
            return;
        }

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

            // If the inventory was empty, highlight the newly added item
            if (itemList.Count == 1)
            {
                SelectItem(0);  // Select the first item (index 0)
            }
        }
    }

    // Function to select an item in the inventory (highlight it)
    public void SelectItem(int index)
    {
        // Deselect previously selected item
        if (selectedItemUI != null)
        {
            DeselectItem();
        }

        // Ensure index is valid
        if (index >= 0 && index < inventoryPanel.childCount)
        {
            // Get the UI element for the new selected item
            selectedItemUI = inventoryPanel.GetChild(index).gameObject;

            // Apply a highlight effect (e.g., change the color)
            UnityEngine.UI.Image selectedItemImage = selectedItemUI.GetComponent<UnityEngine.UI.Image>();
            if (selectedItemImage != null)
            {
                selectedItemImage.color = Color.yellow;  // Highlight color
            }

            selectedItemIndex = index;  // Update the selected item index
        }
    }

    // Function to deselect the currently highlighted item
    public void DeselectItem()
    {
        if (selectedItemUI != null)
        {
            UnityEngine.UI.Image selectedItemImage = selectedItemUI.GetComponent<UnityEngine.UI.Image>();
            if (selectedItemImage != null)
            {
                selectedItemImage.color = Color.white;  // Reset to default color
            }
        }
    }

    // Function to use the selected item
    public void UseSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < itemList.Count)
        {
            InventorySlot selectedSlot = itemList[selectedItemIndex];
            Item selectedItem = selectedSlot.item;

            // Call the corresponding function in PotionFunctions based on the item's name
            PotionFunctions potionFunctions = GetComponent<PotionFunctions>();
            if (potionFunctions != null)
            {
                switch (selectedItem.itemName)
                {
                    case "Health Potion":
                        potionFunctions.useHealthPotion();
                        break;
                    case "to be filled":
                        break;
                    default:
                        UnityEngine.Debug.Log("No function assigned for item: " + selectedItem.itemName);
                        break;
                }
            }

            // Reduce quantity by 1 and check if the item should be removed
            selectedSlot.quantity--;
            if (selectedSlot.quantity <= 0)
            {
                RemoveItem(selectedItemIndex);
            }
            else
            {
                UnityEngine.Debug.Log("Used " + selectedItem.itemName + ". Remaining quantity: " + selectedSlot.quantity);
            }
        }
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {

            // Remove the item from the inventory list
            itemList.RemoveAt(index);

            // Remove the corresponding UI element
            Transform slotUI = inventoryPanel.GetChild(index);
            Destroy(slotUI.gameObject);

            //Debug.Log("Removed item from inventory");

           
              
               
            selectedItemIndex = -1;
            selectedItemUI = null;
            
        }
    }

    public void CycleToNextItem()
    {
        if (itemList.Count == 0) return;  // No items to cycle through

        // Deselect the current item
        DeselectItem();

        // Move to the next item, loop back to the start if at the end
        selectedItemIndex = (selectedItemIndex + 1) % itemList.Count;

        // Select the new item
        SelectItem(selectedItemIndex);

        UnityEngine.Debug.Log("Cycled to next item: " + itemList[selectedItemIndex].item.itemName);
    }


    // Function to add item sprite to the UI
    public void AddItemToUI(Item newItem)
    {
        // Instantiate a new inventory slot (UI Image) in the inventory panel
        GameObject newSlotUI = Instantiate(inventorySlot, inventoryPanel);

        // Set the item's sprite in the UI slot
        UnityEngine.UI.Image itemImage = newSlotUI.GetComponent<UnityEngine.UI.Image>();
        if (itemImage != null)
        {
            itemImage.sprite = newItem.itemIcon;
        }
    }



}
