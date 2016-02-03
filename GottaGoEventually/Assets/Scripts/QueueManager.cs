using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueueManager : MonoBehaviour {

    public static QueueManager singleton;

    public GameObject counter;
    public PersonSpawner spawn;
    public List<Person> people;
    public Person backOfLine;

    // space between people
    public float spacing = 0.3f;
    public int numberOfPeople = 8;
    private Player player;

	// Use this for initialization
	void Start()
    {
        if (singleton == null) singleton = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        counter = GameObject.FindGameObjectWithTag("Counter");
        SpawnPeople();
        //people[numberOfPeople - 1].StartMoving();
        //spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PersonSpawner>();
    }

    public void GameStart()
    {
        people[numberOfPeople - 1].StartMoving();
        spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PersonSpawner>();
    }

    void SpawnPeople()
    {
        people = new List<Person>();
        Person prev = player;
        people.Add(player);
        GameObject person = Resources.Load("Person") as GameObject;
        // generate people in line
        for (int i = 1; i <= numberOfPeople; i++)
        {
            Vector2 newPos = (Vector2)prev.transform.position + Vector2.right * spacing;
            GameObject newP = Instantiate(person);
            newP.transform.position = newPos;
            people.Add(newP.GetComponent<Person>());
            prev.front = people[i];
            people[i].back = prev;
            prev = people[i];
        }
        backOfLine = player;

    }

    public void SpawnNewPerson()
    {
        print(spawn);
        Person p = spawn.SpawnPersonBehind(backOfLine);
        backOfLine = p;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
