using UnityEngine;
using System.Collections;

public class PersonSpawner : MonoBehaviour {

    private GameObject person;

    void Awake()
    {
        person = Resources.Load("Person") as GameObject;
    }

	public Person SpawnPersonBehind(Person front)
    {
        GameObject p = Instantiate(person);
        p.transform.position = transform.position;
        Person pComp = p.GetComponent<Person>();
        pComp.front = front;
        front.back = pComp;
        return pComp;
    }
}
