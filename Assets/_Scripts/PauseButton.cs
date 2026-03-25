using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : ButtonBase
{
    protected override void ButtonPressed()
    {
        if(GameManager.instance.GetGamePauseState())
            ActionManager.OnGamePausedCancelled?.Invoke();
        else
            ActionManager.OnGamePaused?.Invoke();
    }
}