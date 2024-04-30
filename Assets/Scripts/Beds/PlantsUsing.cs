using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlantsUsing : MonoBehaviour
{
    public Transform bed;

    public ObjectsForBed objectsForBed;
    public GameObject plantBtn;

    public bool afterWater = false;

    private BedStatusController.Status currentBedStatus;
    private Vector3[] weedSpawnPos = new Vector3[8] {new Vector3(0.45f, 0f, 0f), new Vector3(0.45f, 0f, -0.50f), new Vector3(0.45f, 0f, 0.50f),
                                                     new Vector3(0f, 0f, 0.50f), new Vector3(-0.45f, 0f, 0.50f), new Vector3(-0.45f, 0f, 0f), 
                                                     new Vector3(-0.45f, 0f, -0.50f), new Vector3(0f, 0f, -0.50f)};
    private Vector3 endWeedPos = new Vector3(0, 0, 0);
    private Vector3 plantPos = new Vector3(0, 0, 0);
    private int spawnCount = 0;
    private int seedIndex = 0;

    private bool isGoodPlant = true;
    private GameObject weed;
    private GameObject plant;
    private float duration = 30f;

    private void Start()
    {
        gameObject.GetComponent<PlantsUsing>().bed = gameObject.transform;
    }

    IEnumerator SeedGrowthRoutine()
    {
        yield return new WaitForSeconds(60);
        if (currentBedStatus == BedStatusController.Status.GROW)
        {
            Destroy(bed.GetChild(0).gameObject);
            StopCoroutine(SpawnWeeds());
            GameObject[] weeds = GameObject.FindGameObjectsWithTag("weed");
            for (int i = 0; i < weeds.Length; i++)
            {
                Destroy(weeds[i]);
            }
            currentBedStatus = BedStatusController.Status.READY;
            SetStatusForBed(BedStatusController.Status.READY);
            if (seedIndex == 3)
            {
                plant = Instantiate(objectsForBed.tomatoPlantPrefab, bed.position + plantPos, Quaternion.identity);
            } else if (seedIndex == 4)
            {
                plant = Instantiate(objectsForBed.cabbagePlantPrefab, bed.position + plantPos, Quaternion.identity);
            }

            plant.transform.parent = bed;
            plant.transform.localScale = new Vector3(3.25f, 3.25f, 3.25f);
            plant.tag = "resultPlant";
        }
    }

    private void PlantSeed()
    {
        if (bed != null)
        {
            objectsForBed.inventoryManager.UsingSeed();
            spawnCount = 1;
            currentBedStatus = BedStatusController.Status.GROW;
            SetStatusForBed(BedStatusController.Status.GROW);

            GameObject startSeed = Instantiate(objectsForBed.seedPrefab, bed.position + plantPos, Quaternion.identity);
            startSeed.transform.parent = bed;
            startSeed.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            StartCoroutine(SeedGrowthRoutine());
            StartCoroutine(SpawnWeeds());
        }
    }

    private void SetStatusForBed(BedStatusController.Status currentStatus)
    {
        bed.GetComponent<BedStatusController>().SetStatus(currentStatus);
    }

    IEnumerator SpawnWeeds()
    {
        while (currentBedStatus == BedStatusController.Status.GROW)
        {
            yield return new WaitForSeconds(10f);

            if (currentBedStatus != BedStatusController.Status.GROW)
            {
                yield break;
            }

            Vector3 randomWeedPos = weedSpawnPos[Random.Range(0, weedSpawnPos.Length)];

            weed = Instantiate(objectsForBed.weedPrefab, bed.position + randomWeedPos, Quaternion.identity);
            weed.tag = "weed";
            weed.transform.parent = bed;
            weed.transform.localScale = new Vector3(2, 2, 2);

            StartCoroutine(WeedTimer());
            isGoodPlant = false;

            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    private void RemoveWeed()
    {
        GameObject[] weeds = GameObject.FindGameObjectsWithTag("weed");
        foreach (GameObject weed in weeds)
        {
            if (weed.transform.parent == objectsForBed.playerOnBed.nowBed)
            {
                Destroy(weed);
            }
        }
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
        GameObject startSeed = Instantiate(objectsForBed.endWeedPrefab, bed.position + plantPos, Quaternion.identity);
        startSeed.transform.parent = bed;
        startSeed.tag = "resultPlant";
        startSeed.transform.localScale = new Vector3(3.25f, 3.25f, 3.25f);
        isGoodPlant = false;
        currentBedStatus = BedStatusController.Status.READY;
        SetStatusForBed(BedStatusController.Status.READY);
    }

    public void AddOrRemoveListenerForInteract(bool isAdd, GameObject bed)
    {
        if (isAdd)
        {
            plantBtn.GetComponent<Button>().onClick.AddListener(delegate {InteractWithBed(bed); });
        } else
        {
            plantBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void InteractWithBed(GameObject bed)
    {
        currentBedStatus = bed.GetComponent<BedStatusController>().GetStatus();
        if (objectsForBed.shovelDurability > 0 && objectsForBed.hoeDurability > 0 && objectsForBed.waterCanDurability > 0)
        {
            switch (currentBedStatus)
            {
                case BedStatusController.Status.EMPTY:
                    if (objectsForBed.waterCanObj.activeSelf || afterWater)
                    {
                        afterWater = true;
                        objectsForBed.playerOnBed.withWater = afterWater;
                        if (objectsForBed.tomatoSeedObj.activeSelf)
                        {
                            PlantSeed();
                            objectsForBed.waterCanDurability--;
                            seedIndex = 3;
                        }
                        if (objectsForBed.cabbageSeedObj.activeSelf)
                        {
                            PlantSeed();
                            objectsForBed.waterCanDurability--;
                            seedIndex = 4;
                        }
                    }
                    break;
                case BedStatusController.Status.GROW:
                    if (objectsForBed.shovelObj.activeSelf)
                    {
                        RemoveWeed();
                        objectsForBed.shovelDurability--;
                    }
                    break;
                case BedStatusController.Status.READY:
                    afterWater = false;
                    objectsForBed.playerOnBed.withWater = afterWater;
                    if (isGoodPlant)
                    {
                        Harvest();
                    }
                    else
                    {
                        Revival();
                    }
                    SwapTerrain();
                    break;
                case BedStatusController.Status.PLOW:
                    if (objectsForBed.hoeObj.activeSelf)
                    {
                        PlowBed();
                        objectsForBed.hoeDurability--;
                    }
                    break;
            }
        }
        else
        {
            StartCoroutine(logShowCor());
        }
    }

    IEnumerator logShowCor()
    {
        GameObject shovelNow = GameObject.FindGameObjectWithTag("shovel");
        GameObject hoeNow = GameObject.FindGameObjectWithTag("hoe");
        GameObject waterCan = GameObject.FindGameObjectWithTag("waterCan");

        if (shovelNow.activeSelf && objectsForBed.shovelDurability < 1)
        {
            shovelNow.SetActive(false);
            objectsForBed.inventoryManager.DeleteItem(objectsForBed.primeSlot);
        } else if (hoeNow.activeSelf && objectsForBed.hoeDurability < 1)
        {
            hoeNow.SetActive(false);
            objectsForBed.inventoryManager.DeleteItem(objectsForBed.primeSlot);
        } else if (waterCan.activeSelf && objectsForBed.waterCanDurability < 1)
        {
            waterCan.SetActive(false);
            objectsForBed.inventoryManager.DeleteItem(objectsForBed.primeSlot);
        }
        objectsForBed.log_Text.text = "Durability is zero, go to the shop";
        objectsForBed.logText.GetComponent<Animation>().Play("textUp");
        yield return new WaitForSeconds(2.5f);
        objectsForBed.logText.GetComponent<Animation>().Play("textDown");
    }

    private void SwapTerrain()
    {
        gameObject.GetComponent<MeshFilter>().mesh = objectsForBed.plantTerrainPrefab.GetComponent<MeshFilter>().mesh;
        gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(128, 0.22f, 128);
    }

    private void Harvest()
    {
        if (seedIndex == 3)
        {
            objectsForBed.statsController.LevelFill(1.3f);
            Destroy(GameObject.FindGameObjectWithTag("resultPlant"));
            int i = Random.Range(6, 9);
            for (int j = 0; j < i; j++)
            {
                objectsForBed.inventoryScript.PickupItem(seedIndex);
            }
            spawnCount = 0;
            seedIndex = 0;
            currentBedStatus = BedStatusController.Status.PLOW;
            SetStatusForBed(BedStatusController.Status.PLOW);
        } else if (seedIndex == 4)
        {
            objectsForBed.statsController.LevelFill(0.6f);
            Destroy(GameObject.FindGameObjectWithTag("resultPlant"));
            int i = Random.Range(6, 9);
            for (int j = 0; j < i; j++)
            {
                objectsForBed.inventoryScript.PickupItem(seedIndex);
            }
            spawnCount = 0;
            seedIndex = 0;
            currentBedStatus = BedStatusController.Status.PLOW;
            SetStatusForBed(BedStatusController.Status.PLOW);
        }
    }

    private void Revival()
    {
        Destroy(GameObject.FindGameObjectWithTag("resultPlant"));
        spawnCount = 0;
        currentBedStatus = BedStatusController.Status.PLOW;
        SetStatusForBed(BedStatusController.Status.PLOW);
    }

    private void PlowBed()
    {
        gameObject.GetComponent<MeshFilter>().mesh = objectsForBed.terrainPrefab.GetComponent<MeshFilter>().mesh;
        gameObject.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(4, 0.22f, 4);
        currentBedStatus = BedStatusController.Status.EMPTY;
        SetStatusForBed(BedStatusController.Status.EMPTY);
    }
}