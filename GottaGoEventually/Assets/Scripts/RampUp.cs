using UnityEngine;
using System.Collections;

public class RampUp : MonoBehaviour
{
    public float emissionRampUp;

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
            part.emissionRate += emissionRampUp;
            part.startSpeed += .1f;
            lastNum = ObstacleSpawn.obstaclesSpawned;
        }
    }
}
