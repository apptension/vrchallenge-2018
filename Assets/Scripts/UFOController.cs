using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class UFOController : MonoBehaviour {

    private Component m_LastPlacedAnchor = null;

	
	// Update is called once per frame
	void Update ()
    {
        

        if (m_LastPlacedAnchor != null)
        {
            // Instantiate Andy model at the hit pose.
            var andyObject = Instantiate(_GetAndyPrefab(), m_LastPlacedAnchor.transform.position,
                m_LastPlacedAnchor.transform.rotation);

            // Make Andy model a child of the anchor.
            andyObject.transform.parent = m_LastPlacedAnchor.transform;

            // Save cloud anchor.
            _HostLastPlacedAnchor();
        }
	}
}
