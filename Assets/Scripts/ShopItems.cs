using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    public Image[] shopItems;
    public int[] shopItemsLevel;
    public StatsController statsController;

    public void CheckItemsInShop(int a)
    {
        for (int i = a; i < shopItems.Length; i++)
        {
            if (shopItemsLevel[i] <= statsController.playerLevel)
            {
                shopItems[i].gameObject.SetActive(true);
            }
            else
            {
                shopItems[i].gameObject.SetActive(false);
            }
        }
    }
}
