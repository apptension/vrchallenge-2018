using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour {
    public GameObject CowPrefab;
    public float scale;
    public int spawnRadius;

    private int m_cowsCount = 5;

    private float _GetCoordWithinRadius() {
        return Random.Range(-this.spawnRadius / 2, this.spawnRadius / 2);
    }

    private Vector3 _GetRandomSpawnPosition() {
        return new Vector3(this._GetCoordWithinRadius(), 0, this._GetCoordWithinRadius());
    }

    private Quaternion _GetRandomRotation() {
        return Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    void SpawnCow() {
        var cowObject = Instantiate(this.CowPrefab, this._GetRandomSpawnPosition(), this._GetRandomRotation());
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < this.m_cowsCount; i++) {
            this.SpawnCow();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
