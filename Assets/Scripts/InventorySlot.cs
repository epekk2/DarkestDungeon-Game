using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;

    // Constructor for inventory slot
    public InventorySlot(Item newItem, int newQuantity)
    {
        item = newItem;
        quantity = newQuantity;
    }

    // Increase the quantity of the item
    public void AddQuantity(int amount)
    {
        quantity += amount;
    }
}
