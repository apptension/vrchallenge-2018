using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using GoogleARCore.Examples.Common;
using UnityEngine.UI;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class CloudAnchorManager : MonoBehaviour {
    
    public string arenaCloudAnchorId;

    public GameObject ARCoreRoot;

    public SnackbarCanvasController snackbarCanvasController;

	// Use this for initialization
	void Start () {
        Debug.Log("asdf");
        ARCoreRoot.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        _UpdateApplicationLifecycle();

        //if (isServer) {
            Touch touch;
            if (Input.touchCount >= 1 && (touch = Input.GetTouch(0)).phase == TouchPhase.Began)
            {
                TrackableHit hit;
                if (Frame.Raycast(touch.position.x, touch.position.y,
                        TrackableHitFlags.PlaneWithinPolygon, out hit))
                {
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    GameManager.instance.anchor = anchor;

                    //XPSession.CreateCloudAnchor(anchor).ThenAction(result =>
                    //{
                    //    if (result.Response != CloudServiceResponse.Success)
                    //    {
                    //        snackbarCanvasController.SnackbarText.text = string.Format("Failed to host cloud anchor: {0}", result.Response);
                    //        return;
                    //    }

                    //    arenaCloudAnchorId = result.Anchor.CloudId;
                    //    snackbarCanvasController.SnackbarText.text = "Successfully placed cloud anchor! " + arenaCloudAnchorId;
                    //});
                }
            }
        //} 



        //if (!isServer && !string.IsNullOrEmpty(arenaCloudAnchorId)) {
        //    snackbarCanvasController.SnackbarText.text = "Resolving cloud anchor. " + arenaCloudAnchorId;

        //    XPSession.ResolveCloudAnchor(arenaCloudAnchorId).ThenAction((System.Action<CloudAnchorResult>)(result =>
        //    {
        //        if (result.Response != CloudServiceResponse.Success)
        //        {
        //            snackbarCanvasController.SnackbarText.text = string.Format("Resolving Error: {0}.", result.Response);
        //            return;
        //        }

        //        snackbarCanvasController.SnackbarText.text = "Successfully resolved cloud anchor! " + arenaCloudAnchorId;

        //        GameManager.instance.anchor = result.Anchor;
        //    }));
        //}
	}

    private void _UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        var sleepTimeout = SleepTimeout.NeverSleep;

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            const int lostTrackingSleepTimeout = 15;
            sleepTimeout = lostTrackingSleepTimeout;
        }

        Screen.sleepTimeout = sleepTimeout;
    }
}
