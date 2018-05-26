using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayController : MonoBehaviour {
    public float runSpeed;
    private float m_rotateSpeed = .001f;
	
    private bool IsWolf(Collider other) {
        return other.tag == "Wolf";
    }

    private Transform CowTransform {
        get { return transform.parent; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsWolf(other)) {
            GetComponentInParent<AnimationControl>().SetAnimation("isRunning");
        }
    }

    void OnTriggerStay(Collider other) {
        if (IsWolf(other)) {
            var runDirection = RunAwayFromWolf(other);
            CowTransform.Translate(runDirection * this.runSpeed * Time.deltaTime, Space.World);
            CowTransform.LookAt(runDirection + CowTransform.position);
        }
    }

    private Vector3 RunAwayFromWolf(Collider wolf) {
        var runDirection = (CowTransform.position - wolf.transform.position).normalized;
        runDirection.y = 0;
        return runDirection;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<AnimationControl>().SetAnimationIdle();
    }
}
