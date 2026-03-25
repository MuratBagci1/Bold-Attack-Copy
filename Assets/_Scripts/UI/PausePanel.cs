using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private void Start()
    {
        ActionManager.OnGamePaused += Show;
        ActionManager.OnGamePausedCancelled += Hide;

        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
