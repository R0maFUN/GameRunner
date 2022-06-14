using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Location
{
    Town,
    Forest
}

[System.Serializable]
public class Road
{
    public GameObject prefab;
    //public GameObject roadObj;
    public Location location;
    public float width;
    public float height;
    public bool hardRoad = false;
    public bool spawnObstacles = true;
}

public class RoadGenerator : Singleton<RoadGenerator>
{
    [SerializeField] public List<Road> roads;
    [SerializeField] int startObstacleSpawning = 3;
    public float maxSpeed = 15f;
    public int maxRoadCount = 1;
    public Location initialLocation = Location.Town;
    public float initialSpeed = 8f;
    public float minX = 0;
    public float maxX = 0;
    public int spawnsAfterLastHardRoad = 0;

    private List<GameObject> spawnedRoadObjects = new List<GameObject>();
    public float currentSpeed = 0f;
    public Location currentLocation;

    // Start is called before the first frame update
    void Start()
    {
        currentLocation = initialLocation;

        ResetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed == 0 || spawnedRoadObjects.Count == 0 || GameManager.instance.isPaused)
            return;

        foreach (GameObject roadObj in spawnedRoadObjects)
        {
            roadObj.transform.position -= new Vector3(0, 0, currentSpeed * Time.deltaTime);
        }
    }

    public void increaseSpeed(float value = 0.2f)
    {
        if (currentSpeed >= maxSpeed)
            return;

        currentSpeed += value;
    }

    private void CreateNextRoad(bool spawnObsticles = false)
    {
        Vector3 position = new Vector3(0, 0, -20);
        if (spawnedRoadObjects.Count > 0)
        {
            GameObject lastRoad = spawnedRoadObjects[spawnedRoadObjects.Count - 1];
            position = lastRoad.transform.position + new Vector3(0, 0, lastRoad.GetComponent<Collider>().bounds.size.z - 0.5f);
        }
        GameObject roadObject = CreateRoad(currentLocation, position, spawnObsticles);

        if (minX == 0 || maxX == 0)
        {
            Transform leftBorderTransform = roadObject.transform.Find("BorderLeft");
            Transform rightBorderTransform = roadObject.transform.Find("BorderRight");

            minX = leftBorderTransform.position.x + 1;
            maxX = rightBorderTransform.position.x;
        }

        spawnedRoadObjects.Add(roadObject);
    }

    private GameObject CreateRoad(Location location, Vector3 position, bool spawnObsticles = false)
    {
        List<Road> roadsInLocation = roads.Where(road => road.location == location).ToList();

        if (GameManager.instance.currentScore > 1)
        {
            int rand = Random.Range(0, 100);
            bool spawnHardRoad = rand > 80;
            if (!spawnHardRoad)
            {
                spawnsAfterLastHardRoad++;
            }
            else
            {
                if (spawnsAfterLastHardRoad < 4)
                    spawnHardRoad = false;
                else
                    spawnsAfterLastHardRoad = 0;
            }

            if (roadsInLocation.Where(road => road.hardRoad == spawnHardRoad).ToList().Count > 0)
                roadsInLocation = roadsInLocation.Where(road => road.hardRoad == spawnHardRoad).ToList();
        }
        else
        {
            roadsInLocation = roadsInLocation.Where(road => road.hardRoad == false).ToList();
        }

        Road road = roadsInLocation[Random.Range(0, roadsInLocation.Count)];
        GameObject roadObj = Instantiate(road.prefab, position, Quaternion.identity);
        roadObj.transform.parent = transform;

        if (spawnObsticles && road.spawnObstacles)
        {
            if (ObsticleGenerator.instance != null)
                ObsticleGenerator.instance.SpawnObsticles(roadObj);
            else Debug.Log("ObsticleGeneratorInstance is null");
        }

        return roadObj;
    }

    public void RespawnFirstRoad()
    {
        if (spawnedRoadObjects.Count == 0)
            return;

        if (ObsticleGenerator.instance != null)
            ObsticleGenerator.instance.RemoveObsticles(spawnedRoadObjects[0]);
        else Debug.Log("ObsticleGeneratorInstance is null");
        Destroy(spawnedRoadObjects[0]);
        spawnedRoadObjects.RemoveAt(0);

        CreateNextRoad(true);
    }

    public void StartLevel()
    {
        currentSpeed = initialSpeed;
        SpawnObsticles();
    }

    public void ResetLevel(bool spawnObsticles = false)
    {
        currentSpeed = 0f;

        while (spawnedRoadObjects.Count > 0)
        {
            if (ObsticleGenerator.instance != null)
                ObsticleGenerator.instance.RemoveObsticles(spawnedRoadObjects[0]);
            else Debug.Log("ObsticleGeneratorInstance is null");
            Destroy(spawnedRoadObjects[0]);
            spawnedRoadObjects.RemoveAt(0);
        }

        for (int i = 0; i < maxRoadCount; ++i)
        {
            CreateNextRoad(spawnObsticles);
        }
    }

    public void SpawnObsticles()
    {
        int i = 0;
        foreach (GameObject spawnedRoad in spawnedRoadObjects)
        {
            if (i < startObstacleSpawning)
            {
                ++i;
                continue;
            }
            if (ObsticleGenerator.instance != null)
                ObsticleGenerator.instance.SpawnObsticles(spawnedRoad);
            else Debug.Log("ObsticleGeneratorInstance is null");
        }
    }
}
