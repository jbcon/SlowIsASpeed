using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawn : MonoBehaviour
{
    protected float spawnDepth = 40;

    public Vector2 numOfSpawners;
    public Vector2 spawnerStart;
    public Vector2 posModifier;
    public GameObject obstacle;
    public List<Transform> spawners;

    public float coolDown = 1f;
    public float LowestSpawnRate = .3f;
    public Transform ER_PlaneCam;

	void Start ()
    {
        spawners = new List<Transform>();

        if (numOfSpawners.x % 2 == 0)
        {
            spawnerStart.x = -1 * posModifier.x / 2;
        }

        if (numOfSpawners.y % 2 == 0)
        {
            spawnerStart.y = -1 * posModifier.y / 2;
        }


        for (int i = 0; i < numOfSpawners.x; i++)
        {
            for (int j = 0; j < numOfSpawners.y; j++)
            {
                //Transform spawner = Resources.Load<Transform>("Spawner");
                //spawner = Instantiate<Transform>(spawner);
                //spawner.name = "Spawner x: " + i + " y: " + j;
                //spawner.position = new Vector3(spawnerStart.x + i * posModifier.x, spawnerStart.y + j * posModifier.y, 40);

                //spawners.Add(spawner);


                //float xDist = i
               // spawner.transform = new Vector3();
            }
        }
        StartCoroutine("SpawnRandom");

    }

    IEnumerator SpawnRandom()
    {
        while (true)
        {
            if(coolDown > LowestSpawnRate)
                coolDown -= .01f;

            float magic = .4f;
            float randX = Random.Range((spawnDepth + 10) * magic * -1f, (spawnDepth + 10) * magic);
            float randY = Random.Range((spawnDepth + 10) * magic * -1f, (spawnDepth + 10) * magic);
            Vector3 spawnSpot = new Vector3(randX, randY, spawnDepth);
            GameObject obs = Instantiate(obstacle, spawnSpot, Quaternion.identity) as GameObject;
            obs.GetComponent<ObstacleMove>().target = ER_PlaneCam;

            yield return new WaitForSeconds(coolDown);
        }
    }
}
