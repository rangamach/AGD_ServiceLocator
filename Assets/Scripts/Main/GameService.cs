using ServiceLocator.Player;
using ServiceLocator.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService player_service { get; private set; }

    [SerializeField] public PlayerScriptableObject playerScriptableObject;

    private void Start()
    {
        player_service = new PlayerService(playerScriptableObject);
    }

    private void Update()
    {
        player_service.Update();
    }

}
