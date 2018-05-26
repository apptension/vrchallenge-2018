using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore.Examples.CloudAnchor;
using UnityEngine.Networking;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class UFOController : NetworkBehaviour {

    private Pose? target = null;
    public GameObject UFOProjectorPrefab;

    [HideInInspector]
    public bool isCatching = false;

	// Update is called once per frame
	void Update ()
    {
        if (isLocalPlayer) {
            return;
        }

        if (GameManager.instance.anchor != null)
        {
            transform.parent = GameManager.instance.anchor.transform;
        }

        TrackableHit hit;
        if (Frame.Raycast(360, 560, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            target = hit.Pose;
        }
	}
	private void FixedUpdate()
	{
        if (!isLocalPlayer)
        {
            return;
        }

        if (target != null && !isCatching)
        {
            float step = 0.15f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, ((Pose)target).position, step);
        }
	}

    public void CatchAnimal()
    {
        if (!isCatching) {
            isCatching = true;
            GameObject ufoProjector = Instantiate(UFOProjectorPrefab, transform);
            Destroy(ufoProjector, 5f);
            Invoke("OnDestroyUFOPRoject", 3.5f);
        }
    }

    private void OnDestroyUFOPRoject()
    {
        isCatching = false;
    }

    private void Start()
    {
        GameManager.instance.PlayerUFO = this.gameObject;
        GameManager.instance.GameStarted += HandleGameStarted;
    }

    void HandleGameStarted(object sender, System.EventArgs e)
    {
        transform.localPosition = new Vector3();
    }
}
