using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour
{
    protected float speed = 10;

    public Transform target;

	// Use this for initialization
	void Start () {
        transform.LookAt(target);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (transform.position.z < -10)
            Destroy(gameObject);

	}
}
