using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionManager
{
    public static Action<GameManager.GameState, WaveSO> OnGameStateChanged;

    public static Action OnXPCollected;

    public static Action OnGamePaused;

    public static Action OnGamePausedCancelled;

    public static Action OnXPTresholdReached;

    public static Action<List<GameObject>, Transform> OnWeaponAdded;

    public static Action<UpgradeSingleUI> OnUISelected;

    public static Action OnUpgradeReachedFour;
}