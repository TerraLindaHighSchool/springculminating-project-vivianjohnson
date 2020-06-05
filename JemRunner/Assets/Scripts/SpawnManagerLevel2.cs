using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManagerLevel2 : MonoBehaviour
{
    public GameObject gemPrefab;
    public GameObject powerupPrefab;
    public GameObject obstaclePrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGemWave(45);
        SpawnPowerup(5);
        SpawnObstacles(45);
        if(SceneManager.GetActiveScene().name.Equals("Level 3"))
        {
            SpawnObstacles(25); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-8, 8);
        float spawnPosZ = Random.Range(10, 420);
        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);
        return randomPos;
    }

    private Vector3 GenerateObstacleSpawnPosition()
    {
        float obsPosX = Random.Range(-8, 8);
        float obsPosZ = Random.Range(15, 420);
        Vector3 randomObsPos = new Vector3(obsPosX, 0, obsPosZ);
        return randomObsPos; 
    }

    void SpawnGemWave(int gemsToSpawn)
    {
        for (int i = 0; i < gemsToSpawn; i++)
        {
            Instantiate(gemPrefab, GenerateSpawnPosition(), gemPrefab.transform.rotation);
        }
    }

    void SpawnPowerup(int powerupsToSpawn)
    {
        for (int i = 0; i < powerupsToSpawn; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    void SpawnObstacles(int obstaclesToSpawn)
    {
        for(int i = 0; i < obstaclesToSpawn; i++)
        {
            Instantiate(obstaclePrefab, GenerateObstacleSpawnPosition(), obstaclePrefab.transform.rotation); 
        }

    }
}
