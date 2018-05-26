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

    private GameObject m_Arena;
    private bool m_isAnchored;

	// Use this for initialization
    void Start () {
        GameManager.instance.GameStarted += HandleGameStarted;
	}

    // Update is called once per frame
    void FixedUpdate () {
        if (m_isAnchored) return;

        var center = PointerRaycast.GetInstance().CurrentTarget;

        if (center != null) {
            if (m_Arena != null)
            {
                m_Arena.transform.position = (Vector3)center;
            }
            else {
                m_Arena = Instantiate(arenaPrefab, center, Quaternion.identity);
            }
        }
	}

    void _AnchorArea() {
        this.m_isAnchored = true;
    }

    void HandleGameStarted(object sender, System.EventArgs e)
    {
        _AnchorArea();
    }
}
