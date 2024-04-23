using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUsing : MonoBehaviour
{
    public GameObject shopBtn;
    public GameObject shopUi;
    public GameObject playerInGame;
    public PlantsUsing plantsUsing;
    public InventoryScript inventoryScript;
    public StatsController statsController;
    public ShopItems shopItems;
    public Image[] itemsInShop;
    public ObjectsForBed objectsForBed;

    private Animation shopAnim;
    private bool shopRunning = false;

    private void Start()
    {
        shopAnim = shopBtn.GetComponent<Animation>();
    }
    public void ShowShopBtn()
    {
        if (shopRunning)
        {
            shopAnim.Play("ShopBtnReverse");
            shopRunning = false;
        }
        else
        {
            shopAnim.Play("ShopButton");
            shopRunning = true;
        }
    }

    public void ShowShopInterface()
    {
        if (shopRunning)
        {
            shopUi.SetActive(true);
            shopItems.CheckItemsInShop(0);
            shopRunning = false;
        }
        else
        {
            shopUi.SetActive(false);
            shopRunning = true;
        }
    }

    public void ShopCategory(string category)
    {
        if (category == "tools")
        {
            for (int i = 0; i < itemsInShop.Length; i++)
            {
                if (i < 3)
                {
                    itemsInShop[i].gameObject.SetActive(true);
                } else
                {
                    itemsInShop[i].gameObject.SetActive(false);
                }
            }
        } else if (category == "seeds")
        {
            for (int i = 0; i < itemsInShop.Length; i++)
            {
                if (i < 3)
                {
                    itemsInShop[i].gameObject.SetActive(false);
                }
                else
                {
                    shopItems.CheckItemsInShop(3);
                }
            }
        } else if (category == "all")
        {
            shopItems.CheckItemsInShop(0);
        }
    }

    public void BuySomething(int i)
    {
        int requiredLevel = 2;
        int nowLevel = statsController.playerLevel;
        switch (i)
        {
            case 1:
                objectsForBed.shovelDurability = 100;
                inventoryScript.BuyItem(0);
                break;

            case 2:
                objectsForBed.hoeDurability = 100;
                inventoryScript.BuyItem(1);
                break;

            case 3:
                objectsForBed.waterCanDurability = 100;
                inventoryScript.BuyItem(2);
                break;

            case 4:
                inventoryScript.BuyItem(5);
                break;

            case 5:
                if (nowLevel >= requiredLevel)
                {
                    inventoryScript.BuyItem(6);
                }
                break;
        }
    }

    public void ClearShopInterface()
    {
        shopAnim.Play("ShopBtnReverse");
        shopUi.SetActive(false);
        shopRunning = false;
    }
}
