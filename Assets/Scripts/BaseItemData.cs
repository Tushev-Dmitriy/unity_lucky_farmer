using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemData : ScriptableObject
{
    public Sprite icon;
    public string title;
    public int price;
    public int durability;
}
