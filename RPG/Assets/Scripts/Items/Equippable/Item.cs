using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Item")]  // This attribute allows you to create instances of this class in the Unity editor
public class Item : ScriptableObject
{
    
    [Header("Item Class")]
    public SlotTag itemClass;

    [Header("Item Image")]
    public Sprite itemImage;

    [Header("Item Prefab")]
    public GameObject itemPrefab;

    [Header("Item ID")]
    public string itemID; 

    [Header("Item Name")]
    public string itemName = "Default Item";
    
    [Header("Item Description")]
    public string itemDescription = "This is a default item description.";
    
    [Header("Item Value")]
    public int itemValue = 0;
    
    [Header("Item Rarity")]
    public ItemRarity itemRarity = ItemRarity.Common;

    [Header("Item Stats")]
    public int itemDamage = 0;
    public int itemArmourPenetration = 0;
    public int itemMaxHealth = 0;
    public int itemDefense = 0;
}
