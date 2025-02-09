using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 19.0f;
    private float spawnRangeZTop = 12.0f;
    private float spawnRangeZLow = -12.0f;
    private float spawnPosX = 24.0f;
    private float spawnPosZ = 12.0f;
    private float startDelay = 2.0f;
    private float interval = 3f;
    private float side = 0f;
    public bool restartSpawn = false;
    private bool spawnActive = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, interval);
        spawnActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Keep  a maximum number of animals
        GameObject[] list = GameObject.FindGameObjectsWithTag("Animal");
        Debug.Log("Number of animals: " + list.Length);

        if (GameObject.Find("Player") == null || (list.Length > 10 && spawnActive))
        {
            Debug.Log("Deactivating spawn manager");
            CancelInvoke("SpawnRandomAnimal");
            spawnActive = false;
        }

        if (list.Length <= 10 && !spawnActive)
        {
            Debug.Log("Activating spawn manager");
            InvokeRepeating("SpawnRandomAnimal", startDelay, interval);
            spawnActive = true;
        }

        if (restartSpawn)
        {
            IncreaseSpawnRate();
        }

    }

    void SpawnRandomAnimal()
    {
        side = Random.Range(1, 4);
        // Left Side
        if (side == 1)
        {
            Vector3 spawnPos = new Vector3(-spawnPosX, 1, Random.Range(spawnRangeZLow, spawnRangeZTop));
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        }
        // Top Side
        else if (side == 2)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1, spawnPosZ);
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        }
        //Right Side
        else
        {
            Vector3 spawnPos = new Vector3(spawnPosX, 1, Random.Range(spawnRangeZLow, spawnRangeZTop));
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        }

    }

    public void IncreaseSpawnRate()
    {
        if (interval > 1)
        {
            CancelInvoke("SpawnRandomAnimal");
            interval -= 0.3f;
            InvokeRepeating("SpawnRandomAnimal", startDelay, interval);
        }
        restartSpawn = false;
    }


}
