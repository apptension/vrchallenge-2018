using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOProjectorCylinder : MonoBehaviour {
    public UFOController UFOController;
    private bool StartCatching = false;

	private void OnTriggerStay(Collider other)
	{
        CatchTarget(other);
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.tag == "Target")
        {
            other.transform.Find("RunCollider").GetComponent<RunAwayController>().StopCatching();
        }
	}

	private void CatchTarget(Collider other) 
    {
        if (other.tag == "Target" && UFOController.isCatching)
        {
            other.transform.Find("RunCollider").GetComponent<RunAwayController>().SetIsCatching();
            float rotate = 90 * Time.deltaTime;
            other.transform.Rotate(new Vector3(rotate, rotate, rotate), Space.Self);
            other.transform.position += Vector3.up * 0.4f * Time.deltaTime;
        }
    }
}
