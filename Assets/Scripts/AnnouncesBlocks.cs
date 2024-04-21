using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class AnnouncesBlocks : MonoBehaviour
{
    public AnnounceUsing announceUsing;
    public GameObject requireTextPrefab;
    public GameObject rewardTextPrefab;
    public InventoryManager inventoryManager;
    public InventoryScript inventoryScript;
    public int imageController = 0;
    private void Start()
    {
        StartCoroutine(LateStart(0.25f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        announceUsing.RewardAdd();
    }

    public void SwapRequireText(int index)
    {
        requireTextPrefab.GetComponent<TextMeshProUGUI>().text = $"Требования: {announceUsing.requireNow[index]}";
    }

    public void SwapRewardText(int index)
    {
        rewardTextPrefab.GetComponent<TextMeshProUGUI>().text = $"Награда: {announceUsing.rewardsNow[index]}";
    }

    public void CheckRequire()
    {
        InventorySlot primeSlot = inventoryManager.primeSlot;
        int index = announceUsing.randomIndex[imageController];
        InventorySlot slot = primeSlot;
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot.title == "tomato" || itemInSlot.title == "cabbage")
        {
            if (announceUsing.infoAboutItem[imageController] == 't' && itemInSlot.count >= announceUsing.requireNow[index])
            {
                inventoryScript.coinCount += announceUsing.rewardsNow[index];
                inventoryScript.coinText.text = inventoryScript.coinCount.ToString();
                itemInSlot.count -= announceUsing.requireNow[index];
                if (itemInSlot.count == 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    primeSlot.GetComponentInChildren<InventoryItem>().RefreshCount();
                }
                Destroy(gameObject);
                announceUsing.AddNewBlocks();
                announceUsing.RewardAdd();
            }
            else if (announceUsing.infoAboutItem[imageController] == 'c' && itemInSlot.count >= announceUsing.requireNow[index])
            {
                inventoryScript.coinCount += announceUsing.rewardsNow[index];
                inventoryScript.coinText.text = inventoryScript.coinCount.ToString();
                itemInSlot.count -= announceUsing.requireNow[index];
                if (itemInSlot.count == 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    primeSlot.GetComponentInChildren<InventoryItem>().RefreshCount();
                }
                Destroy(gameObject);
            }
        }
    }
}
