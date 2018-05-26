using GoogleARCore;
using UnityEngine;


public class WolfController : MonoBehaviour
{
    public float runSpeed;
    public float walkSpeed;
    public float walkThreshold; //min target distance to animate walk
    public float runThreshold;  //min target distance to animate run

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.anchor != null)
        {
            transform.parent = GameManager.instance.anchor.transform;
        }

        if (!PointerRaycast.GetInstance().CurrentTarget.Equals(null))
        {
            this._WalkTo(PointerRaycast.GetInstance().CurrentTarget);
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

    private void Start()
    {
        GameManager.instance.GameStarted += HandleGameStarted;
    }

    void HandleGameStarted(object sender, System.EventArgs e)
    {
        transform.localPosition = new Vector3();
    }
}
