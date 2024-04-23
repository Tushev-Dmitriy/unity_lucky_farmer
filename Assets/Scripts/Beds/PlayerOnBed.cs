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
    
    private bool spawnedNewBed = false;
    public bool withWater = false;

    public GameObject bed;
    public Transform nowBed;

    public PlantsUsing plantUsing;
    public ShopUsing shopUsing;
    public AnnounceUsing announceUsing;
    public InventoryManager inventoryManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "growBed" ||
            other.tag == "readyBed" || other.tag == "plowBed")
        {
            plantUsing.bed = other.gameObject.transform;
            plantUsing.AddOrRemoveListenerForInteract(true, other.gameObject);
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
            if (!spawnedNewBed)
            {
                bed = other.gameObject;
                bedMaterial = other.gameObject.GetComponent<Renderer>().material;
                if (withWater)
                {
                    bedMaterial.color = waterOrange;
                }
                else
                {
                    bedMaterial.color = whiteOrange;
                }
                nowBed = other.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "growBed" || 
            other.tag == "readyBed" || other.tag == "plowBed")
        {
            plantUsing.AddOrRemoveListenerForInteract(false, other.gameObject);
            if (!spawnedNewBed)
            {
                bed = null;
                bedMaterial = other.gameObject.GetComponent<Renderer>().material;
                if (withWater)
                {
                    bedMaterial.color = stockWaterColor;
                } else
                {
                    bedMaterial.color = stockColor;
                }
                nowBed = null;
            }
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

    public void SetSpawnedNewBed(bool value)
    {
        spawnedNewBed = value;
    }
}
