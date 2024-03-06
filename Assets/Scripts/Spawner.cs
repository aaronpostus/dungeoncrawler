using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Ben Keip
public class Spawner : MonoBehaviour
{
    public GameObject[] enemy_prefabs;
    public GameObject[] enemies;
    public int enemy_limit;
    float direction;
    float period;
    [SerializeField] float spawnInterval;

    private void Start()
    {
        period = 0.0f;
    }
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (period > spawnInterval)
        {
            if (enemies.Length < enemy_limit)
            {
                //CarAI new_car = AddCar(RandomLane());
                //new enemy = AddEnemy(position)
            }
            period = 0f;
        }
        period += Time.deltaTime;
    }

    //CarAI AddEnemy(Vector3 position)
    //{
    //    GameObject new_enemy = Instantiate(enemy_prefabs[Random.Range(0, 3)], position, Quaternion.Euler(0, direction, 0), gameObject.transform);
    //    new_enemy.name = $"Enemy {enemies.Length}";

    //    CarAI car_script = new_car.GetComponent<CarAI>();
    //    car_script.car_spawner = this;

    //      CAN ASSIGN gameobjects to components based on pattern above

    //    return car_script;
    //}
}
