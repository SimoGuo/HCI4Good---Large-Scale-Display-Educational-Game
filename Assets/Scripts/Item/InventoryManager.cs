using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> items = new List<Item>();
    public int maxInventorySpace;

    public Transform itemContent;
    public GameObject inventoryItem;

    public UnityEngine.UI.Toggle RemoveItemToggle;
    public InventoryItemController[] inventoryItems;

    private void Awake()
    {
        instance = this; 
    }

    public Boolean Add(Item item)
    {
        if (items.Count < maxInventorySpace)
        {
            items.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        //Clean inventory before opening
        foreach (Transform item  in itemContent) 
        {
            Destroy(item.gameObject);
        }

        
        foreach (var item in items)
        {
            //Instantiate a new object representing each item we have in the "items" list before listing them
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var removeButton = obj.GetComponent<UnityEngine.UI.Button>();
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();
           

            itemName.text = item.itemName;
            itemIcon.sprite = item.sprite;

            if (RemoveItemToggle.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }
        SetInventoryItems();
    }

    public void EnableItemRemove()
    {
        if (RemoveItemToggle.isOn) 
        {
            foreach(Transform item in itemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(Transform item in itemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();

        for(int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }
}
