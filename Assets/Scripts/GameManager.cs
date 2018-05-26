using UnityEngine;
using System.Collections;
using GoogleARCore;

using System.Collections.Generic;       //Allows us to use Lists. 
using System;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.                    //Store a reference to our BoardManager which will set up the level.
	public int level = 3;                                  //Current level number, expressed in game as "Day 1".
	public Component anchor;
    public GameObject UFOPrefab;
    public GameObject WolfPrefab;
    private bool setAnchor = false;

    public event EventHandler GameStarted;
    protected virtual void OnGameStarted() {
        this.GameStarted(this, null);
    }
	
	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			
			//if not, set instance to this
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this)
			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		
		//Call the InitGame function to initialize the first level 
		InitGame();
	}
	
	//Initializes the game for each level.
	void InitGame()
	{
		//Call the SetupScene function of the BoardManager script, pass it current level number.
		
	}
	
	
	
	//Update is called every frame.
	void Update()
	{
        if (GameManager.instance.anchor == null || setAnchor)
        {
            return;
        }

        setAnchor = true;
        //start game
        StartGame();
	}

    private void StartGame()
    {
        GameObject ufo = Instantiate(UFOPrefab);
        ufo.transform.position = GameManager.instance.anchor.transform.position;

        GameObject wolf = Instantiate(WolfPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameManager.instance.anchor.transform);
        wolf.transform.localPosition = new Vector3(0, 0, 0);
        this.OnGameStarted();
    }
}
