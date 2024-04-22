using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string title;
    public int price;
    public bool isStacking;
    public ItemType type;
}

public enum ItemType
{
    Plant,
    Tool
}
