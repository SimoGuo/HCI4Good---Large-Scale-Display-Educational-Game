using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public UnityEngine.UI.Button removeButton;

    [SerializeField] private StatusBarManager statusBarManager;

    private void Awake()
    {
        statusBarManager = GameObject.FindGameObjectWithTag("StatusBar").GetComponent<StatusBarManager>();
    }

    public void RemoveItem()
    {
        InventoryManager.instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                statusBarManager.IncreaseHealth(item.value); 
                break;
            case Item.ItemType.ManaPotion:
                statusBarManager.IncreaseMana(item.value); 
                break;
        }
        RemoveItem();
    }
}