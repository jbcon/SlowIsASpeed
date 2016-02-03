using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

    public Person front;
    public Person back;

    public GameObject spriteObject;

    // time it takes in seconds after the one
    // ahead of them starts moving to move themselves
    public bool isMoving;
    public float oddsOfSwaying = .1f;

    protected Animator animator;

    private bool hasOrdered = false;
    private float movementSpeed = 0.7f;
    private float orderTime;

    private float attentionTime;

    // Use this for initialization
    virtual protected void Awake() {
        isMoving = false;
        attentionTime = Random.Range(1.0f, 2.2f);
        orderTime = Random.Range(2.0f, 6.5f);
        //orderTime = Random.Range(20f, 65f);
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    virtual protected void Update() {
        if (GameManager.instance.gameStarted && !hasOrdered && (!GetComponent<Player>() || isMoving))
        {
            if (front)
            {
                MoveWithSpace(front.transform.position);
            }
            // nobody in front, so they go to the counter
            else
            {
                MoveWithSpace(QueueManager.singleton.counter.transform.position);
            }

            // order the food if nobody in front
            if (!front && ((Vector2)QueueManager.singleton.counter.transform.position - (Vector2)transform.position).magnitude <= QueueManager.singleton.spacing)
            {
                StartCoroutine(OrderFood());
            }
            else
            {
                //StartMoving();
                ShuffleSprite();
            }
        }

        if (isMoving) {
            animator.SetBool("Walking", true);
        }
        else {
            animator.SetBool("Walking", false);
        }
    }

    void MoveWithSpace(Vector2 otherPos)
    {
        float dist = (otherPos - (Vector2)transform.position).magnitude;
        if (dist < QueueManager.singleton.spacing)
        {
            //transform.position = otherPos - Vector2.right * QueueManager.singleton.spacing;
            isMoving = false;
        }

        else
        {
            transform.position = (Vector2)transform.position + Vector2.right * movementSpeed * Time.deltaTime;
            isMoving = true;
        }
    }

    public void StartMoving()
    {

        StartCoroutine(WaitToMove());
    }

    //have person shuffle left and right a bit
    void ShuffleSprite()
    {
        //randomly go to swaying
        float v = Random.value;
        if (v < oddsOfSwaying) {
            animator.SetTrigger("Sway");
        }

        spriteObject.transform.localPosition = new Vector2(Mathf.PerlinNoise(Time.timeSinceLevelLoad, transform.position.x + transform.position.y), transform.position.y) * 0.025f;
    }

    IEnumerator OrderFood()
    {
        if (GetComponent<Player>())
        {
            setVictory();
        }
        SpriteRenderer s = spriteObject.GetComponent<SpriteRenderer>();
        hasOrdered = true;
        QueueManager.singleton.SpawnNewPerson();
        s.color = Color.red;
        yield return new WaitForSeconds(orderTime);
        Destroy(gameObject, 10);
        //yield return null;
        back.front = null;
        s.sortingOrder = 1;
        while (true)
        {
            transform.position = (Vector2)transform.position + Vector2.down * movementSpeed/2.0f * Time.deltaTime;
            yield return null;
        }
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

    public void setVictory()
    {
        GameObject victoryScreen = Resources.Load<GameObject>("VictoryScreen");
        Instantiate(victoryScreen, new Vector3(52.9f, 0, 0), Quaternion.identity);
    }
}
