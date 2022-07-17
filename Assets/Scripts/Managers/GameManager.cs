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
        StartGame();
    }

    public void GameOver()
    {
        SetState(GameState.UiState);
        UiManager.Instance.Announce("Game Over. Thanks for playing!", -1, null);
    }

    private void StartGame()
    {
        UiManager.Instance.Announce("Dicey Dungeon", 2, () =>
        {
            UiManager.Instance.TransitionToShop(ContinueToDiceCustomization);
        });
        
        SetState(GameState.UiState);
    }

    private void SetState(GameState newState)
    {
        if(_state != newState)
            OnStateChanged?.Invoke(newState);
        
        _state = newState;
    }

    public void ContinueToDiceRolling()
    {
        UiManager.Instance.TransitionToDiceRolling(StartNextRound);
    }

    public void ContinueToDiceCustomization()
    {
        UiManager.Instance.TransitionToDiceCustomization(ContinueToDiceRolling);
    }

    public void StartNextRound()
    {
        UiManager.Instance.HideRolling();
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
            if (WaveManager.Instance.IsLastWave)
            {
                GameOver();
                return;
            }
            
            UiManager.Instance.TransitionToShop(ContinueToDiceCustomization);
        });
        
        SetState(GameState.UiState);
    }
}
