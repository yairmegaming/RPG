using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]  // This attribute allows you to create instances of this class in the Unity editor
public class Item : ScriptableObject
{
    [ Header("Item ID")]
    public string itemID; 

    [Header("Item Name")]
    public string itemName = "Default Item";
    
    [Header("Item Description")]
    public string itemDescription = "This is a default item description.";
    
    [Header("Item Class")]
    public string itemType = "Default Item Class";
    
    [Header("Item Value")]
    public int itemValue = 0;
    
    [Header("Item Rarity")]
    public string itemRarity = "Common";
    
    [Header("Item Image")]
    public Sprite itemImage;

    [Header("Item Stats")]
    public int itemDamage = 0;
    public int itemArmourPenetration = 0;
    public int itemMaxHealth = 0;
    public int itemDefense = 0;
}
