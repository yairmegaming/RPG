using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
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
    public int itemHealth = 0;
    public int itemDefense = 0;
}
