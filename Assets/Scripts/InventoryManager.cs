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
    public InventorySlot deleteSlot;

    public GameObject shovelObj;
    public GameObject hoeObj;
    public GameObject waterCanObj;
    public GameObject tomatoSeedObj;
    public GameObject cabbageSeedObj;

    [HideInInspector] public Item item;

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

    public void DeleteItem(InventorySlot slot)
    {
        if (deleteSlot.transform.childCount > 0)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject);
            }
        }
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public void CheckItem()
    {
        if (primeSlot.transform.childCount > 0)
        {
            string name = primeSlot.GetComponentInChildren<InventoryItem>().title;
            if (name == "shovel")
            {
                shovelObj.SetActive(true);
                hoeObj.SetActive(false);
                waterCanObj.SetActive(false);
                tomatoSeedObj.SetActive(false);
                cabbageSeedObj.SetActive(false);
            }
            else if (name == "hoe")
            {
                shovelObj.SetActive(false);
                hoeObj.SetActive(true);
                waterCanObj.SetActive(false);
                tomatoSeedObj.SetActive(false);
                cabbageSeedObj.SetActive(false);
            }
            else if (name == "waterCan")
            {
                shovelObj.SetActive(false);
                hoeObj.SetActive(false);
                waterCanObj.SetActive(true);
                tomatoSeedObj.SetActive(false);
                cabbageSeedObj.SetActive(false);
            } else if (name == "tomatoSeed")
            {
                shovelObj.SetActive(false);
                hoeObj.SetActive(false);
                waterCanObj.SetActive(false);
                tomatoSeedObj.SetActive(true);
                cabbageSeedObj.SetActive(false);
            } else if (name == "cabbageSeed")
            {
                shovelObj.SetActive(false);
                hoeObj.SetActive(false);
                waterCanObj.SetActive(false);
                tomatoSeedObj.SetActive(false);
                cabbageSeedObj.SetActive(true);
            }
        } else
        {
            shovelObj.SetActive(false);
            hoeObj.SetActive(false);
            waterCanObj.SetActive(false);
            tomatoSeedObj.SetActive(false);
            cabbageSeedObj.SetActive(false);
        }
    }
}
