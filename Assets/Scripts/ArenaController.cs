using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class ArenaController : MonoBehaviour {
    public GameObject arenaPrefab;

    private Vector3? m_target = null;
    private GameObject m_Arena;
    private bool m_isAnchored;

	// Use this for initialization
    void Start () {
	}
	
    Vector3? _UpdateCentralPoint() {
        TrackableHit hit;
        if (Frame.Raycast(360, 560, TrackableHitFlags.PlaneWithinPolygon, out hit)) {
            if (m_target == null) {
                m_Arena = Instantiate(arenaPrefab);
            }
            m_target = hit.Pose.position;
        }

        return m_target;
    }

	// Update is called once per frame
	void Update () {
        if (m_isAnchored) return;

        this._UpdateCentralPoint();
        if (m_target != null && m_Arena != null) {
            m_Arena.transform.position = (Vector3)m_target;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _AnchorArea();
        }
	}

    void _AnchorArea() {
        this.m_isAnchored = true;
    }
}
