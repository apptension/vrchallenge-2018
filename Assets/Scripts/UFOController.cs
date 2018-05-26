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

    private Pose target;

	// Update is called once per frame
	void Update ()
    {
        if (isLocalPlayer) {
            return;
        }

        TrackableHit hit;
        if (Frame.Raycast(360, 560, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            target = hit.Pose;
        }

        if (!target.Equals(null)) {
            float step = 0.2f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
	}
}
