using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    void PickUp()
    {
        bool sucessfulPickUp = InventoryManager.instance.Add(item);
        if (sucessfulPickUp)
        {
            Destroy(gameObject);
            inventoryManager.ListItems();
        }
        
    }

    private void OnMouseDown()
    {
        PickUp();
    }
}
