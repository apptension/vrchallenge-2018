using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CowSpawner : NetworkBehaviour {
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
        var position = GameManager.instance.anchor.transform.position + this._GetRandomSpawnPosition();
        var cow = Instantiate(this.CowPrefab, position, this._GetRandomRotation());
        NetworkServer.Spawn(cow);
    }

	// Use this for initialization
	void Start () {
        GameManager.instance.GameStarted += this.SpawnCows;
	}

    void SpawnCows(object sender, System.EventArgs args) {
        Debug.Log("COWS SPAWNED");
        for (int i = 0; i < this.m_cowsCount; i++)
        {
            this.SpawnCow();
        }
    }
}
