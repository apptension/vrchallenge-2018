﻿using UnityEngine;

using UnityEngine.Networking;

using UnityEngine.Networking.Match;

public class CustomNetworkManager : NetworkManager
{
    public GameObject ufoPrefab;
    public GameObject bodyguardPrefab;

    private bool ufoCreated;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        var currentPlayerPrefab = ufoCreated ? bodyguardPrefab : ufoPrefab;
        var player = (GameObject)GameObject.Instantiate(currentPlayerPrefab, Vector3.zero, Quaternion.identity);
        ufoCreated = true;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);


        Debug.Log("Client has requested to get his player added to the game");
    }
}