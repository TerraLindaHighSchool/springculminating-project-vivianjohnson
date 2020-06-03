using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject gemPrefab;
    public GameObject powerupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGemWave(15);
        SpawnPowerup(3); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-8, 8);
        float spawnPosZ = Random.Range(10, 225);
        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);
        return randomPos; 
    }

    void SpawnGemWave(int gemsToSpawn)
    {
        for(int i = 0; i < gemsToSpawn; i++)
        {
            Instantiate(gemPrefab, GenerateSpawnPosition(), gemPrefab.transform.rotation);
        }
    }

    void SpawnPowerup(int powerupsToSpawn)
    {
        for(int i = 0; i < powerupsToSpawn; i++)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation); 
        }
    }
}
