using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayController : MonoBehaviour {
    public float runSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponentInParent<AnimationControl>().SetAnimation("isRunning");
    }

    void OnTriggerStay(Collider other) {
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
        Debug.Log("Uff. Wolf is gone.");
        this.GetComponentInParent<AnimationControl>().SetAnimationIdle();
    }
}
