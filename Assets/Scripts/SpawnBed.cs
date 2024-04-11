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
    private Material bedMaterial;

    public GameObject terrainPrefab;
    public GameObject playerInGame;
    public bool spawnTerrain = false;
    public void SpawnTerrain()
    {
        if (terrainCount < 1)
        {
            terrainPos = playerInGame.transform.position;
            terrainPos.y = 0.512f;
            terrain = Instantiate(terrainPrefab, terrainPos, Quaternion.identity);
            terrain.tag = "spawnBed";
            bedMaterial = terrain.GetComponent<Renderer>().material;
            spawnTerrain = true;
            terrainCount++;
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
            SpawnTerrain();
            terrainPos = playerInGame.transform.position;
            terrainPos.y = 0.512f;
            terrain.transform.position = terrainPos;
        }
    }
}
