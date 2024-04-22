using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemSpawn : MonoBehaviour
{
    public InventoryScript inventoryScript;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            inventoryScript.PickupItem(i);
            inventoryScript.PickupItem(5);
        }
    }
}
