using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class InventoryScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    public GameObject fullInventory;
    public GameObject lowMoney;

    private int coinCount = 100;
    private bool result;
    public int price = 0;
    public TMP_Text coinText;

    public void PickupItem(int id)
    {
        result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Add");
        } else
        {
            StartCoroutine(showFullInventory());
        }
    }

    public void CheckTool()
    {
        inventoryManager.CheckItem();
    }

    public void BuyItem(int id)
    {
        if (coinCount >= price)
        {
            result = inventoryManager.AddItem(itemsToPickup[id]);
            if (result)
            {
                coinCount = coinCount - price;
                coinText.text = coinCount.ToString();
                Debug.Log("Add");
            }
            else
            {
                StartCoroutine(showFullInventory());
            }
        } else
        {
            StartCoroutine(showLowMoney());
        }

    }

    IEnumerator showLowMoney()
    {
        lowMoney.GetComponent<Animation>().Play("LowMoney");
        yield return new WaitForSeconds(2.5f);
        lowMoney.GetComponent<Animation>().Play("LowMoneyReverse");
    }

    IEnumerator showFullInventory()
    {
        fullInventory.GetComponent<Animation>().Play("FullInventory");
        yield return new WaitForSeconds(2.5f);
        fullInventory.GetComponent<Animation>().Play("FullInventoryReverse");
    }

    public void SetValue(int value)
    {
        price = value;
    }
}
