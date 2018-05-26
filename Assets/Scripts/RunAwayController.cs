using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayController : MonoBehaviour {
    public float runSpeed;
    private bool isCatching = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isCatching) {
            return;
        }
        this.GetComponentInParent<AnimationControl>().SetAnimation("isRunning");
    }

    void OnTriggerStay(Collider other) {
        if (isCatching)
        {
            return;
        }
        if (other.tag == "Wolf") {
            var cowTransform = transform.parent;
            var runDirection = (cowTransform.position - other.transform.position).normalized;
            runDirection.y = 0;

            cowTransform.Translate(runDirection * this.runSpeed * Time.deltaTime, Space.World);
            cowTransform.LookAt(runDirection + cowTransform.position);
        }
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
}
