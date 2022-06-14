using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstaclesSpawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> carPrefabs;
    [SerializeField] public List<GameObject> bonusPrefabs;
    [SerializeField] public List<GameObject> spawnPoints;

    [SerializeField] public int carsAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        float probability = Random.Range(0, 1f);
        if (probability <= GameManager.instance.bonusSpawnProbability)
        {
            GameObject bonusPrefab = bonusPrefabs[Random.Range(0, bonusPrefabs.Count)];
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Vector3 spawnPointPos = spawnPoint.transform.position;

            GameObject bonusObj = Instantiate(bonusPrefab, spawnPointPos + new Vector3(0, 1.25f, 0), Quaternion.Euler(0, 0, 0));
            bonusObj.transform.parent = transform;

            spawnPoints.Remove(spawnPoint);
        }

        for (int i = 0; i < carsAmount; ++i)
        {
            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Count)];

            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Vector3 spawnPointPos = spawnPoint.transform.position;

            GameObject carObj = Instantiate(carPrefab, spawnPointPos, Quaternion.Euler(0, -180, 0));
            carObj.transform.parent = transform;

            spawnPoints.Remove(spawnPoint);
        }
    }
}
