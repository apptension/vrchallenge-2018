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

    public GameUIManager gameUI;

	// Use this for initialization
	void Start () {
        //Invoke("SetupARCore", 10f);
        SetupARCore();
	}

    void SetupARCore()
    {
        ARCoreRoot.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        _UpdateApplicationLifecycle();

        Touch touch;
        if (Input.touchCount >= 1 && (touch = Input.GetTouch(0)).phase == TouchPhase.Began)
        {
            TrackableHit hit;
            if (Frame.Raycast(touch.position.x, touch.position.y,
                    TrackableHitFlags.PlaneWithinPolygon, out hit))
            {
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                GameManager.instance.anchor = anchor;
            }
        }
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
