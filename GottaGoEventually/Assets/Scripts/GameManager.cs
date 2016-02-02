using UnityEngine;
using System.Collections;

public class GameManager
{
    public Camera planeCamera;

    private bool instantiated = false;
    private GameManager _instance;
    public GameManager instance
    {
        get
        {
            if (instantiated)
                return _instance;
            else
            {
                instantiated = true;
                return new GameManager();
            }
        }
        private set { _instance = value; }
    }

	void Awake ()
    {
	    
	}
	

	void Update ()
    {
	
	}
}
