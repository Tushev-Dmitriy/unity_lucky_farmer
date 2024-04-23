using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectsForBed : MonoBehaviour
{
    public GameObject tomatoPlantPrefab;
    public GameObject tomatoPrefab;
    public GameObject cabbagePlantPrefab;
    public GameObject cabbagePrefab;
    public GameObject seedPrefab;
    public GameObject weedPrefab;
    public GameObject endWeedPrefab;
    public GameObject plowTerrain;

    public PlayerOnBed playerOnBed;
    public InventorySlot primeSlot;

    public int shovelDurability = 100;
    public int hoeDurability = 100;
    public int waterCanDurability = 100;

    public GameObject shovelObj;
    public GameObject hoeObj;
    public GameObject waterCanObj;
    public GameObject tomatoSeedObj;
    public GameObject cabbageSeedObj;

    public GameObject logText;
    public TMP_Text log_Text;
    public StatsController statsController;
    public InventoryScript inventoryScript;
    public InventoryManager inventoryManager;
    public BedStatusController bedStatusController;
}
