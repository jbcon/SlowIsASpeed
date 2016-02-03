using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour
{
    [HideInInspector]
    public float speed = 10;

    public Transform target;

	// Use this for initialization
	void Start () {
        transform.LookAt(target);
        //GetComponent<Renderer>().material.color = Color.ler
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (transform.position.z < -10)
            Destroy(gameObject);
	}
}
