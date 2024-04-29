using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager inventoryManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryManager.firstSlot == null)
        {
            inventoryManager.firstSlot = gameObject;
            if (inventoryManager.firstSlot.transform.childCount > 0)
            {
                inventoryManager.firstSlotChild = inventoryManager.firstSlot.transform.GetChild(0).gameObject;
            }
        } else
        {
            if (gameObject.transform.childCount > 0)
            {
                inventoryManager.secondSlotChild = gameObject.transform.GetChild(0).gameObject;
            }

            if (inventoryManager.secondSlotChild == null)
            {
                if (inventoryManager.firstSlotChild != null)
                {
                    inventoryManager.firstSlotChild.transform.SetParent(gameObject.transform);
                }
            }
            else
            {
                if (inventoryManager.firstSlotChild != null)
                {
                    inventoryManager.firstSlotChild.transform.SetParent(gameObject.transform);
                    inventoryManager.secondSlotChild.transform.SetParent(inventoryManager.firstSlot.transform);
                }
            }
            inventoryManager.firstSlotChild = null;
            inventoryManager.secondSlotChild = null;
            inventoryManager.firstSlot = null;
        }
    }
}
