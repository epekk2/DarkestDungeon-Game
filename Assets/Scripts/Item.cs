using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;  // Optional, if you want an icon for your item

    // Constructor to create an item
    public Item(string name, Sprite icon = null)
    {
        itemName = name;
        itemIcon = icon;
    }
}