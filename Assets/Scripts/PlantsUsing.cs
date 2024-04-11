using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlantsUsing : MonoBehaviour
{
    public GameObject tomatoPlantPrefab;
    public GameObject tomatoPrefab;
    public GameObject seedPrefab;
    public GameObject weedPrefab;
    public GameObject endWeedPrefab;
    public Transform bed;
    public PlayerOnBed playerOnBed;

    private Vector3[] weedSpawnPos = new Vector3[8] {new Vector3(0.45f, 0f, 0f), new Vector3(0.45f, 0f, -0.50f), new Vector3(0.45f, 0f, 0.50f),
                                                     new Vector3(0f, 0f, 0.50f), new Vector3(-0.45f, 0f, 0.50f), new Vector3(-0.45f, 0f, 0f), 
                                                     new Vector3(-0.45f, 0f, -0.50f), new Vector3(0f, 0f, -0.50f)};
    private Vector3 endWeedPos = new Vector3(0, 0, 0);
    private Vector3 plantPos = new Vector3(0, 0, 0);
    private int spawnCount = 0;

    private enum BedStatus { EMPTY, GROW, READY, PLOW };
    private BedStatus bedStatus = BedStatus.EMPTY;

    private bool isGoodPlant = true;
    private GameObject weed;
    private float duration = 30f;
    IEnumerator SeedGrowthRoutine()
    {
        yield return new WaitForSeconds(60);
        if (bedStatus == BedStatus.GROW)
        {
            Destroy(bed.GetChild(0).gameObject);
            StopCoroutine(SpawnWeeds());
            GameObject[] weeds = GameObject.FindGameObjectsWithTag("weed");
            for (int i = 0; i < weeds.Length; i++)
            {
                Destroy(weeds[i]);
            }
            bedStatus = BedStatus.READY;
            GameObject plant = Instantiate(tomatoPlantPrefab, bed.position + plantPos, Quaternion.identity);
            plant.transform.parent = bed;
            plant.transform.localScale = new Vector3(100, 100, 100);
            Debug.Log("Plant grown");
        }
    }

    private void PlantSeed()
    {
        if (spawnCount < 1)
        {
            bed = playerOnBed.bed.transform;
            if (bed != null)
            {
                Debug.Log("Plant");
                spawnCount = 1;
                bedStatus = BedStatus.GROW;

                GameObject startSeed = Instantiate(seedPrefab, bed.position + plantPos, Quaternion.identity);
                startSeed.transform.parent = bed;
                startSeed.transform.localScale = new Vector3(100, 100, 100);
                StartCoroutine(SeedGrowthRoutine());
                StartCoroutine(SpawnWeeds());
            }
        }
    }

    IEnumerator SpawnWeeds()
    {
        while (bedStatus == BedStatus.GROW)
        {
            yield return new WaitForSeconds(10f);
            Vector3 randomWeedPos = weedSpawnPos[Random.Range(0, weedSpawnPos.Length)];

            weed = Instantiate(weedPrefab, bed.position + randomWeedPos, Quaternion.identity);
            weed.tag = "weed";
            weed.transform.parent = bed;
            weed.transform.localScale = new Vector3(50, 50, 50);

            StartCoroutine(WeedTimer());
            isGoodPlant = false;

            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    private void RemoveWeed()
    {
        Debug.Log("Remove");
        GameObject[] weeds = GameObject.FindGameObjectsWithTag("weed");
        foreach (GameObject weed in weeds)
        {
            Destroy(weed);
        }
        weed = null;
        isGoodPlant = true;
        StopCoroutine(WeedTimer());
    }

    IEnumerator WeedTimer()
    {
        if (isGoodPlant)
        {
            yield break;
        } else
        {
            yield return new WaitForSeconds(duration);
            DestroySeed();
        }
    }

    private void DestroySeed()
    {
        Destroy(bed.GetChild(0).gameObject);
        StopCoroutine(SpawnWeeds());
        GameObject[] weeds = GameObject.FindGameObjectsWithTag("weed");
        for (int i = 0; i < weeds.Length; i++)
        {
            Destroy(weeds[i]);
        }
        GameObject startSeed = Instantiate(endWeedPrefab, bed.position + plantPos, Quaternion.identity);
        startSeed.transform.parent = bed;
        startSeed.transform.localScale = new Vector3(100, 100, 100);
        isGoodPlant = false;
        bedStatus = BedStatus.READY;
    }


    public void InteractWithBed()
    {
        switch (bedStatus)
        {
            case BedStatus.EMPTY:
                PlantSeed();
                break;
            case BedStatus.GROW:
                RemoveWeed();
                break;
            case BedStatus.READY:
                if (isGoodPlant)
                {
                    Harvest();
                } else
                {
                    Revival();
                }
                break;
            case BedStatus.PLOW:
                PlowBed();
                break;
        }
    }

    private void Harvest()
    {
        Debug.Log("Harvest");
        bedStatus = BedStatus.PLOW;
    }

    private void Revival()
    {
        Debug.Log("Revival");
        bedStatus = BedStatus.PLOW;
    }

    private void PlowBed()
    {
        Debug.Log("Plow");
        bedStatus = BedStatus.EMPTY;
    }
}
