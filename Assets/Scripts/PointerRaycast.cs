using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class PointerRaycast : MonoBehaviour {
    private Vector3? m_currentTarget;
    private static PointerRaycast m_instance = null;
	
    public PointerRaycast() {
        PointerRaycast.m_instance = this;
    }

    public static PointerRaycast GetInstance() {
        return PointerRaycast.m_instance;
    }

    // Update is called once per frame
    void Update () {
        TrackableHit hit;
        if (Frame.Raycast(Screen.width / 2, Screen.height / 2, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            m_currentTarget = hit.Pose.position;
        }
	}

    public Vector3 CurrentTarget {
        get { return (Vector3)m_currentTarget; }
    }
}
