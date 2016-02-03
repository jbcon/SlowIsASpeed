using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    public TextMesh text;

    void Start()
    {
        text = GetComponent<TextMesh>();
    }
    void Update()
    {
        text.text = "POINTS: " + ObstacleSpawn.obstaclesSpawned;
    }
}
