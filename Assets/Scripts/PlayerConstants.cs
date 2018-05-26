using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { UFO, Bodyguard }

public class PlayerTypeSelectedEventArgs : System.EventArgs
{
    private PlayerType playerType;

    public PlayerTypeSelectedEventArgs(PlayerType playerType)
    {
        this.playerType = playerType;
    }

    public PlayerType PlayerType
    {
        get { return playerType; }
    }
}

public delegate void PlayerTypeSelectedEventHandler(object sender, PlayerTypeSelectedEventArgs e);