﻿using UnityEngine;
using System.Collections;

public class GameManager : ScriptableObject
{
    public Camera planeCamera;
    public Player player;
    GameObject obstacleSpawner;

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

    public void setGameStarted()
    {
        QueueManager.singleton.GameStart();
        obstacleSpawner.GetComponent<ObstacleSpawn>().enabled = true;
        gameStarted = true;
    }

    

	void MyAwake ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        obstacleSpawner = GameObject.Find("ObstacleSpawner");

        //planeCamera = Resources.Load<Camera>("PlaneCamera");
        //planeCamera = Instantiate<Camera>(planeCamera);
        //planeCamera.transform.position = new Vector3(0f,0f,-10f);

        //planeCamera = GameObject.Find("PlaneObject").GetComponent<Camera>();
	}
}
