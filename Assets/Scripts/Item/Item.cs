using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite sprite;
    public ItemType itemType;

    public enum ItemType
    {
        HealthPotion,
        ManaPotion
    }
}
