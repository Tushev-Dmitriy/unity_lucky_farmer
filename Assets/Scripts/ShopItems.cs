using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    public Image[] shopItems;
    public int[] shopItemsLevel;
    public StatsController statsController;

    public void CheckItemsInShop()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            for (int j = 0; j < shopItemsLevel.Length; j++)
            {
                if (shopItemsLevel[j] < statsController.playerLevel)
                {
                    shopItems[i].gameObject.SetActive(false);
                } else
                {
                    shopItems[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
