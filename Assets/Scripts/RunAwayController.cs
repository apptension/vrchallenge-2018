using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayController : MonoBehaviour {
    public float runSpeed;
    private bool isCatching = true;
    private float m_rotateSpeed = .001f;
	
    private bool IsWolf(Collider other) {
        return other.tag == "Wolf";
    }

    private Transform CowTransform {
        get { return transform.parent; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCatching) {
                    return;
                }
        if (IsWolf(other)) {
            GetComponentInParent<AnimationControl>().SetAnimation("isRunning");
        }
    }

    void OnTriggerStay(Collider other) {
        if (isCatching) return;

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
        if (isCatching)
        {
            return;
        }
        this.GetComponentInParent<AnimationControl>().SetAnimationIdle();
    }

    public void SetIsCatching()
    {
        isCatching = true;
        this.GetComponentInParent<AnimationControl>().SetAnimation("isRunning");
    }

    public void StopCatching()
    {
        isCatching = false;
        this.GetComponentInParent<AnimationControl>().SetAnimationIdle();
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = Quaternion.identity;
    }
}
