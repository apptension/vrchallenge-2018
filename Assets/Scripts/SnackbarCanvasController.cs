using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnackbarCanvasController : MonoBehaviour {

    public Text SnackbarText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowUFOSuccess()
    {
        SnackbarText.text = "Playing as UFO!";
    }

    public void ShowBodyguardSuccess()
    {
        SnackbarText.text = "Playing as Bodyguard!";
    }
}
