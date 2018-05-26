using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.                    //Store a reference to our BoardManager which will set up the level.
    public Component anchor;
    public GameObject UFOPrefab;
    public GameObject WolfPrefab;

    public GameObject startGameMenu;

    private GameObject m_PlayerUFO;
    private GameObject m_PlayerBodyguard;

    private bool isAnchorSet = false;

    public event EventHandler GameStarted;

    public SnackbarCanvasController snackbarCanvasController;

    public CloudAnchorManager cloudAnchorManager;

    protected virtual void OnGameStarted() {
        if (GameStarted != null)
        {
            GameStarted(this, null);
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
    }
	
	//Update is called every frame.
	void Update()
	{
        if (GameManager.instance.anchor == null || isAnchorSet)
        {
            return;
        }

        isAnchorSet = true;

        StartGame();
	}

    private void OnDestroy()
    {
        Debug.Log("Destroy object");
    }

    private void StartGame()
    {
        this.OnGameStarted();
    }

    public void OnStartButtonClicked() {
        startGameMenu.SetActive(false);
    }
}
