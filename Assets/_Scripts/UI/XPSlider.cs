using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class XPSlider : MonoBehaviour
{
    private Slider xpSlider;
    [SerializeField] private GameObject xpPrefab;

    [SerializeField] private float xpTreshold;

    [SerializeField] private float xpValue;
    public int leftoverXP;

    private void Awake()
    {
        xpSlider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        xpSlider.maxValue = xpTreshold;

        ActionManager.OnXPCollected += AddXP;

        CreateXPObjectPool();
    }

    private void AddXP()
    {
        if (xpSlider.value < xpTreshold)
        {
            UpdateXPSlider(xpValue);
            if (xpSlider.value >= xpTreshold)
            {
                ActionManager.OnXPTresholdReached?.Invoke();

                AddLeftOver();
            }
        }
        else
        {
            leftoverXP++;
        }
    }

    private void UpdateXPSlider(float value)
    {
        xpSlider.value += value;
    }

    private void AddLeftOver()
    {
        xpSlider.value = 0;
        UpdateXPSlider(leftoverXP);
        leftoverXP = 0;
    }

    private void CreateXPObjectPool()
    {
        PoolManager.Instance.CreatePool<ExperiencePoint>(xpPrefab.GetComponent<ExperiencePoint>(), 5);
    }
}