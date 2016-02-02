using UnityEngine;
using System.Collections;

public class ObstacleSpawn : MonoBehaviour
{
    public float spawnDepth;
    public Vector2 numOfSpawners;

	void Start ()
    {
        for (int i = 0; i < numOfSpawners.x; i++)
        {
            for (int j = 0; i < numOfSpawners.y; i++)
            {
                Transform spawner = Resources.Load<Transform>("Spawner");
                spawner = Instantiate<Transform>(spawner);
                spawner.name = "Spawner x: " + i + " y: " + j;

                
                //float xDist = i 
                //spawner.transform = new Vector3();
            }
        }
	}
}
