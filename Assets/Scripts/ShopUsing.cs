using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUsing : MonoBehaviour
{
    public GameObject shopBtn;
    public GameObject shopUi;
    public GameObject shovelPrefab;
    public GameObject playerInGame;
    public PlantsUsing plantsUsing;
    public TMP_Text coinText;
    public int price = 0;
    public InventoryScript inventoryScript;

    private Animation shopAnim;
    private bool shopRunning = false;
    private int coinCount = 100;

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

    public void BuyShovel()
    {
        if (BuySubject())
        {
            shovelPrefab.SetActive(true);
            plantsUsing.shoveldurability = 100;
            inventoryScript.PickupItem(0);
        }
    }

    public void SetValue(int value)
    {
        price = value;
    }

    public void ClearShopInterface()
    {
        shopAnim.Play("ShopBtnReverse");
        shopUi.SetActive(false);
        shopRunning = false;
    }

    private bool BuySubject()
    {
        if (coinCount > price) 
        {
            coinCount = coinCount - price;
            coinText.text = coinCount.ToString();
            return true;
        } else
        {
            return false;
        }
    }
}
