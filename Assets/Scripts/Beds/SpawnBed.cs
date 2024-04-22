using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class SpawnBed : MonoBehaviour
{
    private Vector3 terrainPos = Vector3.zero;
    private GameObject terrain;
    private Color stockColor = new Color(0.52f, 0.39f, 0.26f);
    private Color whiteGreen = new Color(0.73f, 1f, 0.73f);
    private Material bedMaterial;

    public GameObject terrainPrefab;
    public GameObject playerInGame;
    public TerrainColor terrainColor;

    public GameObject spawnButton;
    public GameObject endSpawnButton;
    public GameObject plantButton;

    public GameObject shovelObj;

    private bool isSpawning = false;
    private bool canSpawn = true;

    public void SpawnOrStopTerrain()
    {
        if (shovelObj.activeSelf)
        {
            if (!isSpawning && canSpawn)
            {
                terrainPos = playerInGame.transform.position;
                terrainPos.y = 0.512f;
                terrain = Instantiate(terrainPrefab, terrainPos, Quaternion.identity, playerInGame.transform);
                terrain.tag = "spawnBed";
                bedMaterial = terrain.GetComponent<Renderer>().material;
                isSpawning = true;
                bedMaterial.color = whiteGreen;
                spawnButton.SetActive(false);
                plantButton.SetActive(false);
                endSpawnButton.SetActive(true);
            }
            else if (canSpawn)
            {
                isSpawning = false;
                MakeTerrainWithoutParent();
                spawnButton.SetActive(true);
                plantButton.SetActive(true);
                endSpawnButton.SetActive(false);
            }
        }
    }

    private void MakeTerrainWithoutParent()
    {
        if (terrain != null)
        {
            terrain.transform.parent = null;
            terrain.tag = "emptyBed";
            bedMaterial.color = stockColor;
        }
    }

    public void SetCanSpawn(bool value)
    {
        canSpawn = value;
    }
}
