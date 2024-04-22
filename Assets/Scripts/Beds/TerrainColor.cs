using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainColor : MonoBehaviour
{
    private Material bedMaterial;
    private Color whiteRed = new Color(1f, 0.46f, 0.46f);
    private Color whiteGreen = new Color(0.73f, 1f, 0.73f);
    private Color stockColor = new Color(0.52f, 0.39f, 0.26f);
    public SpawnBed spawnBed;

    private void Start()
    {
        Collider terrainCollider = GetComponentInChildren<Collider>();
        bedMaterial = gameObject.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "emptyBed" && other.tag != "shop" && other.tag != "water")
        {
            bedMaterial.color = whiteGreen;
        } else
        {
            bedMaterial.color = whiteRed;
            spawnBed.SetCanSpawn(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "shop")
        {
            bedMaterial.color = whiteRed;
            spawnBed.SetCanSpawn(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "emptyBed" || other.tag == "shop" || other.tag == "water")
        {
            bedMaterial.color = whiteGreen;
            spawnBed.SetCanSpawn(true);
        }
    }
}
