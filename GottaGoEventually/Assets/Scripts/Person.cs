using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

    public Person front;
    public Person back;

    // time it takes in seconds after the one
    // ahead of them starts moving to move themselves
    public float movementSpeed = 0.4f;

    public bool isMoving;

    private float attentionTime;

    // Use this for initialization
    virtual protected void Awake () {
        isMoving = false;
        attentionTime = Random.Range(0.2f, 1.2f);
	}

	// Update is called once per frame
	virtual protected void Update () {
	    if (isMoving)
        {
            transform.position = (Vector2)transform.position + Vector2.right * movementSpeed * Time.deltaTime;

            if (front)
            {
                MoveWithSpace(front.transform.position);
            }
            // nobody in front, so they go to the counter
            else
            {
                MoveWithSpace(QueueManager.singleton.counter.transform.position);
            }
        }
        
	}

    void MoveWithSpace(Vector2 otherPos)
    {
        float dist = (otherPos - (Vector2)transform.position).magnitude;

        if (dist < QueueManager.singleton.spacing)
        {
            transform.position = otherPos - Vector2.right * QueueManager.singleton.spacing;
            isMoving = false;
        }
    }

    public void StartMoving()
    {
        StartCoroutine(WaitToMove());
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(attentionTime);
        isMoving = true;
        if (back && !back.GetComponent<Player>())
        {
            back.StartMoving();
        }
    }
}
