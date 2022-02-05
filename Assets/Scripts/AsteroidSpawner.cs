using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private string[] asteroidPrefabs;
    private float secondsBetweenAsteroids = 0.5f;
    [SerializeField] private Vector2 forceRange;
    private float timer;
    private Camera mainCamera;
    private ObjectPooler objectPooler;

    private void Start()
    {
        mainCamera = Camera.main;
        objectPooler = ObjectPooler.instance;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <=0) 
        {
            SpawnAsteroid();
            timer += secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroid() 
    {
        int side = Random.Range(0,4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side) 
        {
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f,1f));
                break;
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f),1f);
                break;
            case 3:
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;

        string selectedAsteroid = asteroidPrefabs[Random.Range(0,asteroidPrefabs.Length)];

        GameObject asteroidInstance = objectPooler.SpawnFromPool(selectedAsteroid, worldSpawnPoint,Quaternion.identity);

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * Random.Range(forceRange.x,forceRange.y);
    }

    public void IncreaseDifficulty()
    {
        if (secondsBetweenAsteroids != 0f) { secondsBetweenAsteroids -= 0.001f; }
        forceRange = new Vector2(forceRange.x + 0.01f, forceRange.y + 0.01f);
    }

    public void ResetDifficulty() 
    {
        secondsBetweenAsteroids = 0.5f;
        forceRange = new Vector2(4f,6f);
    }
}
