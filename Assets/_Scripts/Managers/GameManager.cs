using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Cooldown,
        Paused,
        GamePlaying
    }

    [SerializeField] private List<WaveSO> waveList;
    [SerializeField] private float cooldownTimer;
    [SerializeField] private float cooldownTimerMax;

    private PlayerInputActions inputActions;

    private WaveSO currentWave;
    private bool isGameStarted;
    private bool isGamePaused;
    private GameState currentGameState;

    public GameState CurrentGameState
    {
        get
        {
            return currentGameState;
        }
        set
        {
            currentGameState = value;
        }
    }

    public static GameManager instance;

    private void Awake()
    {
        instance = this;

        inputActions = new PlayerInputActions();

        inputActions.UI.Enable();

        inputActions.UI.Pause.performed += PauseGameToggle;
    }

    private void Start()
    {
        CurrentGameState = GameState.Cooldown;

        ChangeWave();

        ActionManager.OnGamePaused += PauseGame;
        ActionManager.OnNewWave += PauseGame;
        ActionManager.OnXPTresholdReached += PauseGame;
        ActionManager.OnGamePausedCancelled += ContinueGame;
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Cooldown)
        {
            if (cooldownTimer <= 0 && !isGameStarted)
            {
                isGameStarted = true;

                CurrentGameState = GameState.GamePlaying;

                ActionManager.OnGameStateChanged?.Invoke(CurrentGameState, currentWave);
            }
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void PauseGameToggle(InputAction.CallbackContext context)
    {
        if (isGamePaused)
        {
            ContinueGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }

    public bool GetGamePauseState()
    {
        return isGamePaused;
    }

    private void ChangeWave()
    {
        currentWave = waveList[0];

        waveList.Remove(currentWave);

        ActionManager.OnNewWave?.Invoke();
    }
}