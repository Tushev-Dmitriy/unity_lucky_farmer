using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnBed : MonoBehaviour
{
    private Material bedMaterial;
    private Color whiteOrange = new Color(1, 0.91f, 0.65f);
    private Color stockColor = new Color(0.52f, 0.39f, 0.26f);
    private bool spawnedNewBed = false;

    public GameObject bed;
    public Transform nowBed;

    public PlantsUsing plantUsing;
    public ShopUsing shopUsing;
    public AnnounceUsing announceUsing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "growBed" || 
            other.tag == "readyBed" || other.tag == "plowBed")
        {
            if (!spawnedNewBed)
            {
                bed = other.gameObject;
                bedMaterial = other.gameObject.GetComponent<Renderer>().material;
                bedMaterial.color = whiteOrange;
                nowBed = other.transform;
            }
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "growBed" || 
            other.tag == "readyBed" || other.tag == "plowBed")
        {
            if (!spawnedNewBed)
            {
                bed = null;
                bedMaterial = other.gameObject.GetComponent<Renderer>().material;
                bedMaterial.color = stockColor;
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
        }
    }

    public void SetSpawnedNewBed(bool value)
    {
        spawnedNewBed = value;
    }
}
