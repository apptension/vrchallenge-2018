﻿using UnityEngine;
using System.Collections;
using GoogleARCore;

using System.Collections.Generic;       //Allows us to use Lists. 
using System;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using GoogleARCore.Examples.CloudAnchor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.                    //Store a reference to our BoardManager which will set up the level.
    public int level = 3;                                  //Current level number, expressed in game as "Day 1".
    public Component anchor;
    public GameObject UFOPrefab;
    public GameObject WolfPrefab;

    public GameObject startGameMenu;

    private GameObject m_PlayerUFO;
    private GameObject m_PlayerBodyguard;

    private bool setAnchor = false;

    public event EventHandler GameStarted;

    public MatchInfo matchInfo;

    public SnackbarCanvasController snackbarCanvasController;

    public CloudAnchorManager cloudAnchorManager;

    public GameObject PlayerUFO {
        get { return m_PlayerUFO; }
        set {
            m_PlayerUFO = value;
            snackbarCanvasController.ShowUFOSuccess();
            OnPlayerSet();
        }
    }

    public GameObject PlayerBodyguard
    {
        get { return m_PlayerBodyguard; }
        set
        {
            m_PlayerBodyguard = value;
            snackbarCanvasController.ShowBodyguardSuccess();
            OnPlayerSet();
        }
    }

    protected virtual void OnGameStarted() {
        this.GameStarted(this, null);
    }

    private void OnPlayerSet() {
        if (m_PlayerUFO != null) {
            startGameMenu.SetActive(false);
        }
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

	}

    private void Start()
    {
        Debug.Log("Start game");
        NetworkManager.singleton.StartMatchMaker();

    }

    public void CreateInternetMatch(string matchName)
    {
        NetworkManager.singleton.matchMaker.CreateMatch(matchName, 2, true, "", "", "", 0, 0, OnInternetMatchCreate);
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

    private void OnDestroy()
    {
        Debug.Log("Destroy object");
    }

    private void StartGame()
    {
        GameObject ufo = Instantiate(UFOPrefab, GameManager.instance.anchor.transform);
        ufo.transform.localPosition = new Vector3();;

        //GameObject wolf = Instantiate(WolfPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameManager.instance.anchor.transform);
        //wolf.transform.localPosition = new Vector3(0, 0, 0);
        this.OnGameStarted();
    }

    public void OnUFOPlayerTypeButtonClicked() {
        CreateInternetMatch("default");
    }

    public void OnBodyguardPlayerTypeButtonClicked() {
        NetworkManager.singleton.matchMaker.ListMatches(0, 1, "default", true, 0, 0, OnInternetMatchList);
    }

    private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Create match succeeded");

            NetworkServer.Reset();

            this.matchInfo = matchInfo;
            NetworkServer.Listen(matchInfo, 9000);

            NetworkManager.singleton.StartHost(matchInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                Debug.Log("A list of matches was returned ");

                //join the last server (just in case there are two...)
                NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
            }
            else
            {
                Debug.Log("No matches found");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker. " + extendedInfo);
        }
    }

    private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Able to join a match");

            MatchInfo hostInfo = matchInfo;
            NetworkManager.singleton.StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }

    private void OnInternetMatchDestroy(bool success, string extendedInfo)
    {

    }
}
