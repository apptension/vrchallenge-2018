using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEffect : MonoBehaviour {

    public GameObject prefabEffect;

	// Use this for initialization
	void Start () 
    {
        Invoke("CreateEffect", 2f);
	}

    void CreateEffect()
    {
        GameObject effect = Instantiate(prefabEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);
    }
}
