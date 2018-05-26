using UnityEngine;
using GoogleARCore.Examples.CloudAnchor;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    private Component m_localAnchor;

    [SyncVar]
    public PlayerType m_type;

    private void Start()
    {
        if (isLocalPlayer) {
            if (isServer)
            {
                m_type = PlayerType.UFO;
            }
            else
            {
                m_type = PlayerType.Bodyguard;
            }
        }
    }

    void Update()
    {
        if (m_type == PlayerType.UFO && GameManager.instance.PlayerUFO != this.gameObject)
        {
            
            GameManager.instance.PlayerUFO = this.gameObject;
        }

        if (m_type == PlayerType.Bodyguard && GameManager.instance.PlayerBodyguard != this.gameObject)
        {
            GameManager.instance.PlayerBodyguard = this.gameObject;
        }

        if (!isLocalPlayer) {
            return;
        }
    }
}
