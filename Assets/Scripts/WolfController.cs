using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore.Examples.CloudAnchor;

using Input = GoogleARCore.InstantPreviewInput;
using UnityEngine.Networking;

public class WolfController : NetworkBehaviour
{
    public float runSpeed;
    public float walkSpeed;
    public float walkThreshold; //min target distance to animate walk
    public float runThreshold;  //min target distance to animate run
    private Pose target;

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) {
            return;
        }

        TrackableHit hit;
        if (Frame.Raycast(360, 560, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            target = hit.Pose;
        }

        if (!target.Equals(null))
        {
            this._WalkTo(target.position);
        }
    }

    private bool _ShouldRunTo(float targetDistance) {
        return targetDistance > this.runThreshold;
    }

    private bool _ShouldWalkTo(float targetDistance) {
        return targetDistance > this.walkThreshold;
    }

    void _WalkTo(Vector3 targetPosition) {
        var targetDistance = Vector3.Distance(targetPosition, transform.position);
        this._SetAnimationBasedOnTargetDistance(targetDistance);

        var moveSpeed = this._ShouldRunTo(targetDistance) ? this.runSpeed : this.walkSpeed;
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        transform.LookAt(targetPosition);
    }

    private void _SetAnimationBasedOnTargetDistance(float distance) {
        var animationControl = this.GetComponent<AnimationControl>();
        if (this._ShouldRunTo(distance)) {
            animationControl.SetAnimation("isRunning");
            return;
        }
        if (this._ShouldWalkTo(distance)) {
            animationControl.SetAnimation("isWalking");
            return;
        }
        animationControl.SetAnimationIdle();
    }
}
