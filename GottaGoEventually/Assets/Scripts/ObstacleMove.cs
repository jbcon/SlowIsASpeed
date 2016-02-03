using UnityEngine;
using System.Collections;

public class ObstacleMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, -1);
        if (transform.position.z < -10)
            Destroy(gameObject);

	}
}
