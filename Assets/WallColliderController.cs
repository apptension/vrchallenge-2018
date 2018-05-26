using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderController : MonoBehaviour
{
    private float m_rotateSpeed = .5f;
    private float m_speed = .1f;

    private bool IsWall(Collision collision)
    {
        return collision.gameObject.tag == "Fence";
    }

    void OnCollisionStay(Collision collision)
    {
        if (IsWall(collision))
        {
            transform.Rotate(new Vector3(0, m_rotateSpeed, 0));
            transform.Translate(Vector3.forward * this.m_speed * Time.deltaTime, Space.World);
        }
    }
}
