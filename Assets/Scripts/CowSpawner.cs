﻿using UnityEngine;

public class CowSpawner : MonoBehaviour {
    public GameObject CowPrefab;
    public float spawnRadius;

    private int m_cowsCount = 30;

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
        var position = this._GetRandomSpawnPosition();
        var cowObject = Instantiate(this.CowPrefab, new Vector3(0, 0, 0), this._GetRandomRotation());
        cowObject.transform.parent = GameManager.instance.anchor.transform;
        cowObject.transform.localPosition = this._GetRandomSpawnPosition();
    }

	// Use this for initialization
	void Start () {
        GameManager.instance.GameStarted += this.SpawnCows;
	}

    void SpawnCows(object sender, System.EventArgs args) {
        Debug.Log("COWS SPAWNED");
        for (int i = 0; i < this.m_cowsCount; i++)
        {
            Invoke("SpawnCow", i/4.0f);
        }
    }
}
