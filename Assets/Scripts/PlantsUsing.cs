using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlantsUsing : MonoBehaviour
{
    public GameObject tomatoPlantPrefab;
    public GameObject tomatoPrefab;
    public GameObject weedPrefab;
    public GameObject endWeedPrefab;
    public PlayerOnBed playerOnBed;

    private Vector3 spawnPos = new Vector3 (-2.1f, 0.52f, 4.5f);
    private Vector3 weedSpawnPos = new Vector3 (-1.6f, 0.52f, 4.1f);
    private int spawnCount = 0;

    private bool isEmptyBed = true;
    private bool isGrowBed = false;
    private bool isReadyBed = false;
    private bool isPlowBed = false;

    private bool weedOnBed = false;
    private bool goodPlant = true;

    private void PlantTomato()
    {
        if (spawnCount < 1)
        {
            Debug.Log(1);
            spawnCount = 1;
            isEmptyBed = false;
            isGrowBed = true;
            StartCoroutine(plantSpawn());
        }
    }

    IEnumerator plantSpawn()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(plantController());
        yield return new WaitForSeconds(50);
        ReadyBed();
    }

    IEnumerator plantController() //единичный спаун
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        Debug.Log(21);
        SpawnWeed(); //остается static
    }

    public void EmptyBed() 
    {
        if (playerOnBed.GetBedStatus(1))
        {
            GameObject shovel = GameObject.FindGameObjectWithTag("shovel");
            if (shovel.activeSelf && shovel != null)
            {
                PlantTomato();
            }
        }
    }

    private void SpawnWeed()
    {
        Debug.Log(3);
        Instantiate(weedPrefab, weedSpawnPos, Quaternion.identity);
    }

    private void ReadyBed() //не меняется тег
    {
        isGrowBed = false;
        isReadyBed = true;
        if (playerOnBed.GetBedStatus(2))
        {
            Debug.Log(4);
            if (goodPlant)
            {
                Instantiate(tomatoPlantPrefab, spawnPos, Quaternion.identity);
                spawnCount = 0;
            }
            else if (!goodPlant)
            {
                Instantiate(endWeedPrefab, spawnPos, Quaternion.identity);
                spawnCount = 0;
            }
        }
    }

    public bool GetBedStatus(int i)
    {
        switch (i)
        {
            case 1:
                return isEmptyBed;
            case 2:
                return isGrowBed;
            case 3:
                return isReadyBed;
            case 4:
                return isPlowBed;
            default:
                return false;
        }
    }
}
