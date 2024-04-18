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

    [HideInInspector] public string nameOfSlot;
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

    public void UsingSeed()
    {
        if (primeSlot.transform.childCount > 0)
        {
            InventoryItem primeItem = primeSlot.GetComponentInChildren<InventoryItem>();
            if (nameOfSlot == "tomatoSeed" || nameOfSlot == "cabbageSeed")
            {
                primeItem.count--;
                if (primeItem.count == 0)
                {
                    Destroy(primeItem.gameObject);
                    tomatoSeedObj.SetActive(false);
                    cabbageSeedObj.SetActive(false);
                } else
                {
                    primeItem.RefreshCount();
                }
            }
        }
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
            nameOfSlot = primeSlot.GetComponentInChildren<InventoryItem>().title;
            if (nameOfSlot == "shovel")
            {
                shovelObj.SetActive(true);
                hoeObj.SetActive(false);
                waterCanObj.SetActive(false);
                tomatoSeedObj.SetActive(false);
                cabbageSeedObj.SetActive(false);
            }
            else if (nameOfSlot == "hoe")
            {
                shovelObj.SetActive(false);
                hoeObj.SetActive(true);
                waterCanObj.SetActive(false);
                tomatoSeedObj.SetActive(false);
                cabbageSeedObj.SetActive(false);
            }
            else if (nameOfSlot == "waterCan")
            {
                shovelObj.SetActive(false);
                hoeObj.SetActive(false);
                waterCanObj.SetActive(true);
                tomatoSeedObj.SetActive(false);
                cabbageSeedObj.SetActive(false);
            } else if (nameOfSlot == "tomatoSeed")
            {
                shovelObj.SetActive(false);
                hoeObj.SetActive(false);
                waterCanObj.SetActive(false);
                tomatoSeedObj.SetActive(true);
                cabbageSeedObj.SetActive(false);
            } else if (nameOfSlot == "cabbageSeed")
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
