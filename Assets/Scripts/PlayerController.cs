using UnityEngine;
using GoogleARCore.Examples.CloudAnchor;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    private Component m_localAnchor;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (m_localAnchor != GameManager.instance.anchor) {
            m_localAnchor = GameManager.instance.anchor;

            transform.position = m_localAnchor.transform.position;
            transform.rotation = m_localAnchor.transform.rotation;
        }

        var m_x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var m_z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, m_x, 0);
        transform.Translate(0, 0, m_z);
    }
}
