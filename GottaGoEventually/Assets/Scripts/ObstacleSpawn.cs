using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawn : MonoBehaviour
{
    protected float spawnDepth = 60;

    public GameObject obstacle;
    public GameObject obstacle2;
    public GameObject obstacle3;

    public float coolDown = .9f;
    public float LowestSpawnRate = .3f;
    
    public static int obstaclesSpawned { get; set; }
    public Transform ER_PlaneCam;

	void Start ()
    {
        //spawners = new List<Transform>();

        //if (numOfSpawners.x % 2 == 0)
        //{
        //    spawnerStart.x = -1 * posModifier.x / 2;
        //}

        //if (numOfSpawners.y % 2 == 0)
        //{
        //    spawnerStart.y = -1 * posModifier.y / 2;
        //}


        //for (int i = 0; i < numOfSpawners.x; i++)
        //{
        //    for (int j = 0; j < numOfSpawners.y; j++)
        //    {
        //        //Transform spawner = Resources.Load<Transform>("Spawner");
        //        //spawner = Instantiate<Transform>(spawner);
        //        //spawner.name = "Spawner x: " + i + " y: " + j;
        //        //spawner.position = new Vector3(spawnerStart.x + i * posModifier.x, spawnerStart.y + j * posModifier.y, 40);

        //        //spawners.Add(spawner);


        //        //float xDist = i
        //       // spawner.transform = new Vector3();
        //    }
        //}
        StartCoroutine("SpawnRandom");

    }

    IEnumerator SpawnRandom()
    {
        while (true)
        {
            Spawn();
            if (Random.Range(0, 1 + 30/obstaclesSpawned) == 0)
                Spawn();

            yield return new WaitForSeconds(coolDown);
        }
    }

    void Spawn()
    {
        obstaclesSpawned++;

        if (coolDown > LowestSpawnRate)
            coolDown -= .005f;

        float magic = .4f;
        float randX = Random.Range((spawnDepth + 10) * magic * -1f, (spawnDepth + 10) * magic);
        float randY = Random.Range((spawnDepth + 10) * magic * -1f, (spawnDepth + 10) * magic);
        Vector3 spawnSpot = new Vector3(randX, randY, spawnDepth);


        GameObject obs;
        int rand = Random.Range(0, 16);
        if ( rand == 0)
            obs = Instantiate(obstacle2, spawnSpot, obstacle2.transform.rotation) as GameObject;
        else if (rand == 1)
            obs = Instantiate(obstacle3, spawnSpot, obstacle3.transform.rotation) as GameObject;
        else
            obs = Instantiate(obstacle, spawnSpot, obstacle.transform.rotation) as GameObject;


        obs.GetComponent<ObstacleMove>().target = ER_PlaneCam;
        obs.GetComponent<ObstacleMove>().speed = 10 + obstaclesSpawned / 10f;
    }
}
