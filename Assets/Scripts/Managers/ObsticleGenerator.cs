using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UnityEngine;

//[System.Serializable]
//public class Obstacle
//{
//    public enum Type
//    {
//        Car,
//        Bus,
//        TrafficAccident,
//        Fencing,
//        //LampPost,
//        //Glass,
//    }

//    public Type type;
//    public List<Location> locations;
//    public GameObject prefab;
//    public float probability;
//    public int maxAmountPerRoad; // depends on defficulty level
//    public int x;
//    public int z;
//    public int coordinatesWidth;
//    public int coordinatesHeight;
//    public int strength;
//}

[System.Serializable]
public class Obstacle
{
    public enum Type
    {
        OneCar,
        TwoCars,
        ThreeCars,
        OneCarOneTrafficAccident
    }

    public Type type;
    public List<Location> locations;
    public GameObject prefab;
    public float probability;
    public int maxAmountPerRoad; // depends on defficulty level
    public int x;
    public int z;
    public int coordinatesWidth;
    public int coordinatesHeight;
    public int strength;
}

public class ObsticleGenerator : Singleton<ObsticleGenerator>
{
    /*[SerializeField]*/ public int verticalLinesAmount = 4;
    [SerializeField] public int horizontalLinesAmount = 5;
    [SerializeField] public int initialStrengthPerRoad = 4;
    [SerializeField] private List<Obstacle> obstacles;

    private Dictionary<GameObject, List<GameObject>> spawnedObstacles = new Dictionary<GameObject, List<GameObject>>(); // <roadObj, List<obsticleObj>>
    private int maxStrengthPerRoad; // depends on defficulty level

    private void Awake()
    {
        maxStrengthPerRoad = initialStrengthPerRoad;
        instance = GetComponent<ObsticleGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObsticles(GameObject road)
    {
        List<Obstacle> availableObstacles = obstacles.Where(obstacle => obstacle.locations.Contains(RoadGenerator.instance.currentLocation)).ToList();
        float minZ = road.GetComponent<MeshRenderer>().bounds.min.z;
        float maxZ = road.GetComponent<MeshRenderer>().bounds.max.z;
        float lineHeight = (maxZ - minZ) / horizontalLinesAmount;

        for (int i = 0; i < horizontalLinesAmount; ++i)
        {
            bool shouldSpawn = Random.Range(0, 1f) <= 0.8f;
            if (!shouldSpawn)
                continue;

            Obstacle obstacleToSpawn = availableObstacles[Random.Range(0, availableObstacles.Count)];

            UnityEngine.Vector3 position = new UnityEngine.Vector3(0, 0, minZ + lineHeight * i + lineHeight / 2);
            GameObject obstacleObj = Instantiate(obstacleToSpawn.prefab, position, obstacleToSpawn.prefab.transform.rotation);
            obstacleObj.transform.parent = road.transform;

            if (!spawnedObstacles.ContainsKey(road))
                spawnedObstacles.Add(road, new List<GameObject>());
            spawnedObstacles[road].Add(obstacleObj);
        }
    }

    public void RemoveObsticles(GameObject road)
    {
        if (!spawnedObstacles.ContainsKey(road))
            return;

        List<GameObject> obstacles = spawnedObstacles[road];
        foreach (GameObject obstacle in obstacles)
            Destroy(obstacle);

        spawnedObstacles.Remove(road);
    }
}
