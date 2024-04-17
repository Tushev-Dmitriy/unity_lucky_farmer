using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUsing : MonoBehaviour
{
    public GameObject shopBtn;
    public GameObject shopUi;
    public GameObject playerInGame;
    public PlantsUsing plantsUsing;
    public InventoryScript inventoryScript;

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
            shopRunning = false;
        }
        else
        {
            shopUi.SetActive(false);
            shopRunning = true;
        }
    }

    public void BuySomething(int i)
    {
        switch (i)
        {
            case 1:
                plantsUsing.shovelDurability = 100;
                inventoryScript.BuyItem(0);
                break;

            case 2:
                plantsUsing.hoeDurability = 100;
                inventoryScript.BuyItem(1);
                break;

            case 3:
                plantsUsing.waterCanDurability = 100;
                inventoryScript.BuyItem(2);
                break;

            case 4:
                inventoryScript.BuyItem(5);
                break;

            case 5:
                inventoryScript.BuyItem(6);
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
