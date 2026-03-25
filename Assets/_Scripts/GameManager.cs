using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private WaveSO currentWave;
    private bool isGameStarted;
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
    }

    private void Start()
    {
        CurrentGameState = GameState.Cooldown;

        currentWave = waveList[0];

        waveList.Remove(currentWave);
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

    private void ChangeWave()
    {
        currentWave = waveList[0];

        waveList.Remove(currentWave);
    }
}