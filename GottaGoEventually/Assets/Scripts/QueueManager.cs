using UnityEngine;
using System.Collections;

public class QueueManager : MonoBehaviour {

    public static QueueManager singleton;

    public GameObject counter;
    public Person[] people;

    // space between people
    public float spacing = 0.3f;
    public int numberOfPeople = 8;
    private Player player;

	// Use this for initialization
	void Awake()
    {
        if (singleton == null) singleton = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        counter = GameObject.FindGameObjectWithTag("Counter");
        SpawnPeople();
        people[numberOfPeople - 1].StartMoving();
    }


    void SpawnPeople()
    {
        people = new Person[numberOfPeople];
        GameObject person = Resources.Load("Person") as GameObject;
        Person prev = player;
        // generate people in line
        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector2 newPos = (Vector2)prev.transform.position + Vector2.right * spacing;
            GameObject newP = Instantiate(person);
            newP.transform.position = newPos;
            people[i] = newP.GetComponent<Person>();
            prev.front = people[i];
            people[i].back = prev;
            prev = people[i];
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
