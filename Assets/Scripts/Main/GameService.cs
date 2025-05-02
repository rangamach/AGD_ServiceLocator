using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService player_service { get; private set; }
    public SoundService sound_service { get; private set; }

    [SerializeField] private UIService uiService;
    public UIService UIService => uiService;

    [SerializeField] public PlayerScriptableObject playerScriptableObject;
    [SerializeField] public SoundScriptableObject soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        player_service = new PlayerService(playerScriptableObject);
        sound_service = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
    }

    private void Update()
    {
        player_service.Update();
    }

}
