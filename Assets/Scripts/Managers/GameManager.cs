using System;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    UiState,
    PlacementState,
    WaveState
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState _state = GameState.UiState;

    public GameState State => _state;
    public UnityEvent<GameState> OnStateChanged { get; } = new();

    private void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        WaveCleared();
    }

    private void SetState(GameState newState)
    {
        if(_state != newState)
            OnStateChanged?.Invoke(newState);
        
        _state = newState;
    }

    public void StartNextRound()
    {
        UiManager.Instance.HideShop();
        UiManager.Instance.DisableNextButton();
        
        UiManager.Instance.Announce("Place your towers!", 2, () =>
        {
            UiManager.Instance.EnableNextButton("Start Wave", () =>
            {
                WaveManager.Instance.StartNextWave(WaveCleared);
                UiManager.Instance.DisableNextButton();
                SetState(GameState.WaveState);
            }); 
        });
        
        SetState(GameState.PlacementState);
    }

    public void WaveCleared()
    {
        UiManager.Instance.Announce("Wave Cleared!", 2, () =>
        {
            UiManager.Instance.TransitionToShop(StartNextRound);
        });
        
        SetState(GameState.UiState);
    }
}
