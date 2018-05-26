using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnGameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        GameManager.instance.GameStarted += HandleGameStarted;
	}

    void HandleGameStarted(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
