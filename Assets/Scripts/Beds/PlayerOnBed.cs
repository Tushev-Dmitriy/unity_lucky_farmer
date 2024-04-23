using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnBed : MonoBehaviour
{
    private Material bedMaterial;
    private Color whiteOrange = new Color(1, 0.91f, 0.65f);
    private Color stockColor = new Color(0.52f, 0.39f, 0.26f);
    private Color waterOrange = new Color(1, 0.81f, 0.26f);
    private Color stockWaterColor = new Color(0.28f, 0.21f, 0.15f);
    
    public bool withWater = false;

    public GameObject bed;
    public Transform nowBed;

    public ShopUsing shopUsing;
    public AnnounceUsing announceUsing;
    public InventoryManager inventoryManager;
    private PlantsUsing plantUsing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "growBed" ||
            other.tag == "readyBed" || other.tag == "plowBed")
        {
            plantUsing = other.gameObject.GetComponent<PlantsUsing>();
            plantUsing.bed = other.gameObject.transform;
            plantUsing.AddOrRemoveListenerForInteract(true, other.gameObject);
            withWater = plantUsing.afterWater;
            if (withWater)
            {
                other.gameObject.GetComponent<Renderer>().material.color = waterOrange;
            } else
            {
                other.gameObject.GetComponent<Renderer>().material.color = whiteOrange;
            }
            nowBed = other.gameObject.transform;
        }

        if (other.tag == "shop")
        {
            shopUsing.ShowShopBtn();
        }

        if (other.tag == "announce")
        {
            announceUsing.ShowAnnounceUI();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "growBed" ||
            other.tag == "readyBed" || other.tag == "plowBed")
        {
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            withWater = plantUsing.afterWater;
            if (withWater)
            {
                bedMaterial.color = waterOrange;
            }
            else
            {
                bedMaterial.color = whiteOrange;
            }
            nowBed = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "growBed" || 
            other.tag == "readyBed" || other.tag == "plowBed")
        {
            plantUsing = other.gameObject.GetComponent<PlantsUsing>();
            plantUsing.AddOrRemoveListenerForInteract(false, other.gameObject);
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            withWater = plantUsing.afterWater;
            if (withWater)
            {
                bedMaterial.color = stockWaterColor;
            }
            else
            {
                bedMaterial.color = stockColor;
            }
            nowBed = null;
        }

        if (other.tag == "shop")
        {
            shopUsing.ShowShopBtn();
            shopUsing.ClearShopInterface();
        }

        if (other.tag == "announce")
        {
            announceUsing.ShowAnnounceUI();
            inventoryManager.CheckItem();
        }
    }
}
