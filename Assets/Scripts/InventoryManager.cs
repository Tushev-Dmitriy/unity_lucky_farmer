using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public int maxStackItem = 16;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public InventorySlot primeSlot;

    [HideInInspector] public GameObject shovelObj;
    [HideInInspector] public GameObject hoeObj;
    [HideInInspector] public GameObject waterCanObj;
    [HideInInspector] public Item item;
    private void Start()
    {
        shovelObj = GameObject.FindGameObjectWithTag("shovel");
        hoeObj = GameObject.FindGameObjectWithTag("hoe");
        waterCanObj = GameObject.FindGameObjectWithTag("waterCan");
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackItem && itemInSlot.item.isStacking == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public void checkItem(Item newItem)
    {
        item = newItem;
        if (item.title == "shovel")
        {
            shovelObj.SetActive(true);
            hoeObj.SetActive(false);
            waterCanObj.SetActive(false);
        }
        else if (item.title == "hoe")
        {
            shovelObj.SetActive(false);
            hoeObj.SetActive(true);
            waterCanObj.SetActive(false);
        }
        else if (item.title == "waterCan")
        {
            shovelObj.SetActive(false);
            hoeObj.SetActive(false);
            waterCanObj.SetActive(true);
        }
    }
}
