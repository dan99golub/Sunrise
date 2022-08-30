using System;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPlayers : MonoBehaviour
{
    private List<Player> _players= new List<Player>();

    public static ContainerPlayers Instance; 

    public Action<Player> AddedPlayer;

    private void Awake()
    {
        Instance = Instance ?? this;
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);   
        AddedPlayer?.Invoke(player);
    }
}
