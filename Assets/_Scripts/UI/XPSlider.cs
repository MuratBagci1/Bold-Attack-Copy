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

    private bool isLeftoverCoroutineStarted;
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
        if (!(xpSlider.value >= xpTreshold))
        {
            xpSlider.value++;
        }
        else if (!isLeftoverCoroutineStarted)
        {
            ActionManager.OnXPTresholdReached?.Invoke();

            leftoverXP++;

            isLeftoverCoroutineStarted = true;

            StartCoroutine(AddLeftOver());
        }
        else
        {
            leftoverXP++;
        }
    }

    private IEnumerator AddLeftOver()
    {
        while (xpSlider.value >= xpTreshold)
        {
            yield return null;
        }

        xpSlider.value += leftoverXP;
        leftoverXP = 0;
        isLeftoverCoroutineStarted = false;
    }

    private void CreateXPObjectPool()
    {
        PoolManager.Instance.CreatePool<ExperiencePoint>(xpPrefab.GetComponent<ExperiencePoint>(), 5);
    }
}