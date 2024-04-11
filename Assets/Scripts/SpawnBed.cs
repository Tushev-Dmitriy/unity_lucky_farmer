using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class SpawnBed : MonoBehaviour
{
    private Vector3 terrainPos = Vector3.zero;
    private int terrainCount = 0;
    private GameObject terrain;
    private Color stockColor = new Color(0.52f, 0.39f, 0.26f);
    private Color whiteGreen = new Color(0.73f, 1f, 0.73f);
    private Material bedMaterial;

    public GameObject terrainPrefab;
    public GameObject playerInGame;
    public TerrainColor terrainColor;
    private bool canSpawn;
    private int i = 0;

    public bool spawnTerrain = false;
    public void SpawnTerrain()
    {
        Debug.Log(canSpawn);
        if (terrainCount < 1/* && canSpawn*/)
        {
            terrainPos = playerInGame.transform.position;
            terrainPos.y = 0.512f;
            terrain = Instantiate(terrainPrefab, terrainPos, Quaternion.identity, playerInGame.transform);
            terrain.tag = "spawnBed";
            bedMaterial = terrain.GetComponent<Renderer>().material;
            spawnTerrain = true;
            terrainCount++;
            bedMaterial.color = whiteGreen;
        }
    }

    public void SpawnTerrainAgain()
    {
        TerrainColor color = terrain.GetComponent<TerrainColor>();
        Destroy(color);
        terrain.tag = "emptyBed";
        terrain = null;
        terrainPos = Vector3.zero;
        terrainCount = 0;
        spawnTerrain = false;
        bedMaterial.color = stockColor;
    }

    private void Update()
    {
        if (spawnTerrain)
        {
            if (i == 1)
            {
                canSpawn = terrainColor.canSpawn;
                Debug.Log(canSpawn);
            }
            else
            {
                canSpawn = true;
                i++;
            }
            SpawnTerrain();
            terrainPos = playerInGame.transform.position;
            terrainPos.y = 0.512f;
            terrain.transform.position = terrainPos;
        }
    }

    public void MakeTerrainWithoutParent()
    {
        terrain.transform.parent = null;
    }
}
