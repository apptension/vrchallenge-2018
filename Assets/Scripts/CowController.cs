using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour {
    public GameObject CowPrefab;
    public float scale;
    public float spawnRadius;

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
            this.SpawnCow();
        }
    }
}
