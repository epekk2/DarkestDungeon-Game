using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryClass inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new InventoryClass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
