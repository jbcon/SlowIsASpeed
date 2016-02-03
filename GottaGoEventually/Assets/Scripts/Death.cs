using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider other)
    {
        print("hittttttttt");
        if (other.gameObject.tag == "Obstacle")
        {
            GameManager.instance.setLoss("Crash");
        }
    }
}
