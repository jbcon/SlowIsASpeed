using UnityEngine;
using System.Collections;

public class RampUp : MonoBehaviour
{
    ParticleSystem part;

    int lastNum;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        lastNum = ObstacleSpawn.obstaclesSpawned;
    }
    void Update()
    {
        if (lastNum < ObstacleSpawn.obstaclesSpawned)
        {
            part.emissionRate += 1;
            part.startSpeed += .1f;
            lastNum = ObstacleSpawn.obstaclesSpawned;
        }
    }
}
