using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointer : MonoBehaviour {
    public GameObject movePointerPrefab;
    private GameObject m_pointer;

    void FixedUpdate () {
        var target = PointerRaycast.GetInstance().CurrentTarget;

        if (target == null) return;

        if (!m_pointer) {
            m_pointer = Instantiate(movePointerPrefab, target.Value, Quaternion.identity);
        }
        m_pointer.transform.position = target.Value;
	}
}
