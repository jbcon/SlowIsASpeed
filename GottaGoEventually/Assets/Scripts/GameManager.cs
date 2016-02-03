using UnityEngine;
using System.Collections;

public class GameManager : ScriptableObject
{
    public Camera planeCamera;
    public Player player;
    GameObject obstacleSpawner;
    PhoneMovementLogic phone;

    static private bool instantiated = false;
    static private GameManager _instance;
    static public GameManager instance
    {
        get
        {
            if (instantiated)
                return _instance;
            else
            {
                instantiated = true;
                _instance = CreateInstance<GameManager>();
                _instance.MyAwake();
                return _instance;
            }
        }
        private set { _instance = value; }
    }

    public bool phoneActive = false;
    public bool phoneDown = true;
    public bool gameStarted = false;
    public bool gameOver = false;

    public void setGameStarted()
    {
        QueueManager.singleton.GameStart();
        obstacleSpawner.GetComponent<ObstacleSpawn>().enabled = true;
        gameStarted = true;
    }

    public void setVictory()
    {
        GameObject victoryScreen = Resources.Load<GameObject>("BestDamnSandwich");
        Instantiate(victoryScreen, new Vector3(52.9f, 0, 0), Quaternion.identity);
        gameOver = true;
    }

    public void setLoss()
    {
        GameObject loseScreen = Resources.Load<GameObject>("GameOver");
        Instantiate(loseScreen);//, new Vector3(52.9f, 0, 0), Quaternion.identity);
        gameOver = true;
        phone.ActivateLowerPhone();
    }

    void MyAwake ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        obstacleSpawner = GameObject.Find("ObstacleSpawner");
        phone = GameObject.Find("Phone").GetComponent<PhoneMovementLogic>();
        //planeCamera = Resources.Load<Camera>("PlaneCamera");
        //planeCamera = Instantiate<Camera>(planeCamera);
        //planeCamera.transform.position = new Vector3(0f,0f,-10f);

        //planeCamera = GameObject.Find("PlaneObject").GetComponent<Camera>();
	}

    public void Reset()
    {
        phoneActive = false;
        phoneDown = true;
        gameStarted = false;
        gameOver = false;
        MyAwake();
    }
}
