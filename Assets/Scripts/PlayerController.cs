using UnityEngine;
using GoogleARCore.Examples.CloudAnchor;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    


    private Component m_localAnchor;

    public PlayerType m_type;

    public GameObject WolfPrefab;
    public GameObject UFOPrefab;

    private bool IsUFO
    {
        get { return m_type == PlayerType.UFO; }
    }

    private bool IsBodyguard
    {
        get { return m_type == PlayerType.Bodyguard; }
    }

    private void Start()
    {
        if (isLocalPlayer && isServer || !isLocalPlayer && !isServer)
        {
            m_type = PlayerType.UFO;
        } 
        else if (isLocalPlayer && !isServer || !isLocalPlayer && isServer) {
            m_type = PlayerType.Bodyguard;
        }

        GameManager.instance.GameStarted += HandleGameStarted;
    }

    void Update()
    {
        if (IsUFO && GameManager.instance.PlayerUFO != this.gameObject)
        {
            GameManager.instance.PlayerUFO = this.gameObject;
        }

        if (IsBodyguard && GameManager.instance.PlayerBodyguard != this.gameObject)
        {
            GameManager.instance.PlayerBodyguard = this.gameObject;
        }
    }

    private void HandleGameStarted(object sender, System.EventArgs e)
    {
        if (!isServer)
        {
            return;
        }

        if (IsBodyguard)
        {
            GameObject wolf = Instantiate(WolfPrefab);
            wolf.transform.parent = GameManager.instance.anchor.transform;
            wolf.transform.localPosition = new Vector3();
            NetworkServer.SpawnWithClientAuthority(wolf, connectionToClient);
        }

        if (IsUFO)
        {
            GameObject ufo = Instantiate(UFOPrefab);
            ufo.transform.parent = GameManager.instance.anchor.transform;
            ufo.transform.localPosition = new Vector3();
            NetworkServer.Spawn(ufo);
        }
    }
}
