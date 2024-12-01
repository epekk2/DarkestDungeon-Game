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

    private Camera mainCamera;
    private PotionFunctions potionFunctions;

    private ItemPickup touchingItem;
    public Item currentTouchingItem = null;

    void Start()
    {
        mainCamera = Camera.main;
        potionFunctions = GetComponent<PotionFunctions>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTouchingItem != null)
        {
            AddItem(currentTouchingItem);
            // Find and destroy the game object that has the ItemPickup component with this item
            ItemPickup[] itemPickups = FindObjectsOfType<ItemPickup>();
            foreach (ItemPickup pickup in itemPickups)
            {
                if (pickup.item == currentTouchingItem)
                {
                    Destroy(pickup.gameObject);
                    break;
                }
            }
            currentTouchingItem = null;
        }

        if (selectedItemIndex >= 0 && Input.GetMouseButtonDown(0))  // Left mouse click
        {
            UseSelectedItemAtMousePosition();
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

    private void CheckForItemPickup()
    {
        // Check for colliders in a small radius around the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            // Assuming items have a component that holds their Item data
            ItemPickup itemPickup = collider.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                AddItem(itemPickup.item);
                Destroy(itemPickup.gameObject);
                break;
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
    private void UseSelectedItemAtMousePosition()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < itemList.Count)
        {
            // Get mouse position in world coordinates
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            InventorySlot selectedSlot = itemList[selectedItemIndex];
            Item selectedItem = selectedSlot.item;

            // Use item at mouse position
            if (potionFunctions != null)
            {
                bool itemUsed = true;
                switch (selectedItem.itemName)
                {
                    case "Health Potion":
                        potionFunctions.useHealthPotion(mousePos);
                        break;
                    case "Fire Potion":
                        potionFunctions.useFirePotion(mousePos);
                        break;
                    case "Water Potion":
                        potionFunctions.useWaterPotion(mousePos);
                        break;
                    default:
                        //Debug.Log("No function assigned for item: " + selectedItem.itemName);
                        break;
                }

                // Only reduce quantity if the item was successfully used
                if (itemUsed)
                {
                    selectedSlot.quantity--;
                    if (selectedSlot.quantity <= 0)
                    {
                        RemoveItem(selectedItemIndex);
                    }
                    else
                    {
                        //Debug.Log("Used " + selectedItem.itemName + ". Remaining quantity: " + selectedSlot.quantity);
                    }
                }
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
