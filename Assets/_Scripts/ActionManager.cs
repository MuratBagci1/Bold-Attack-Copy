using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionManager
{
    public static Action<GameManager.GameState, WaveSO> OnGameStateChanged;
}