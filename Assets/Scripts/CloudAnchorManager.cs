using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using GoogleARCore.Examples.Common;
using UnityEngine.UI;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class CloudAnchorManager : NetworkBehaviour {

    [SyncVar]
    public string arenaCloudAnchorId;

    public SnackbarCanvasController snackbarCanvasController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!string.IsNullOrEmpty(arenaCloudAnchorId)) {
            Debug.Log("Cloud anchor ID: " + arenaCloudAnchorId);
        }

        if (!isServer) {
            return;
        }

        Touch touch;
        if (Input.touchCount >= 1 && (touch = Input.GetTouch(0)).phase == TouchPhase.Began)
        {
            TrackableHit hit;
            if (Frame.Raycast(touch.position.x, touch.position.y,
                    TrackableHitFlags.PlaneWithinPolygon, out hit))
            {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                GameManager.instance.anchor = anchor;

                XPSession.CreateCloudAnchor(anchor).ThenAction(result =>
                {
                    if (result.Response != CloudServiceResponse.Success)
                    {
                        snackbarCanvasController.SnackbarText.text = string.Format("Failed to host cloud anchor: {0}", result.Response);
                        return;
                    }

                    arenaCloudAnchorId = result.Anchor.CloudId;
                    snackbarCanvasController.SnackbarText.text = "Successfully placed cloud anchor! " + arenaCloudAnchorId;
                });
            }
        }
	}
}
