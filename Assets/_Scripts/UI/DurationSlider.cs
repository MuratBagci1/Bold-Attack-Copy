using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationSlider : MonoBehaviour
{
    private Slider xpSlider;

    [SerializeField] private float playDuration;

    private void Awake()
    {
        xpSlider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        xpSlider.maxValue = playDuration;
    }
}