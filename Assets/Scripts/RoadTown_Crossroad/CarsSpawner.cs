using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Left,
    Right
}

struct Car
{
    public GameObject obj;
    public Direction direction;
}

public class CarsSpawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> carPrefabs;
    [SerializeField] public float carsSpeed = 8f;
    [SerializeField] public int carsAmount = 4;

    [SerializeField] private GameObject leftSpawnTrigger;
    [SerializeField] private GameObject rightSpawnTrigger;
    [SerializeField] private GameObject leftRespawnPoint;
    [SerializeField] private GameObject rightRespawnPoint;

    private List<Car> spawnedCars = new List<Car>();


    // Start is called before the first frame update
    void Start()
    {
        initialSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedCars.Count < carsAmount * 2)
            return;

        foreach (Car car in spawnedCars)
        {
            car.obj.transform.position += new Vector3(carsSpeed * Time.deltaTime * (car.direction == Direction.Right ? 1 : -1), 0, 0);

            if (car.direction == Direction.Right)
            {
                if (car.obj.transform.position.x >= rightSpawnTrigger.transform.position.x)
                    car.obj.transform.position = leftRespawnPoint.transform.position;
            }
            else
            {
                if (car.obj.transform.position.x <= leftSpawnTrigger.transform.position.x)
                    car.obj.transform.position = rightRespawnPoint.transform.position;
            }
        }
    }

    private void initialSpawn()
    {
        float xDiff = (rightRespawnPoint.transform.position.x - leftRespawnPoint.transform.position.x) / carsAmount;
        for (int i = 0; i < carsAmount; ++i)
        {
            GameObject rightCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Count)];
            GameObject rightCarObj = Instantiate(rightCarPrefab, rightRespawnPoint.transform.position - new Vector3(xDiff * i, 0, 0), Quaternion.Euler(0, -90, 0));
            rightCarObj.transform.parent = transform;

            Car rightCar = new Car();
            rightCar.direction = Direction.Left;
            rightCar.obj = rightCarObj;

            spawnedCars.Add(rightCar);

            GameObject leftCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Count)];
            GameObject leftCarObj = Instantiate(leftCarPrefab, leftRespawnPoint.transform.position + new Vector3(xDiff * i, 0, 0), Quaternion.Euler(0, 90, 0));
            leftCarObj.transform.parent = transform;

            Car leftCar = new Car();
            leftCar.direction = Direction.Right;
            leftCar.obj = leftCarObj;

            spawnedCars.Add(leftCar);
        }
    }
}
