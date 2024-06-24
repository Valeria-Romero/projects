using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatGauge : MonoBehaviour
{
    [SerializeField] private Image needle;
    [Range(0,1)] public float currentHeatAmount = 1f;
    [SerializeField] private float heatAmount;
    [SerializeField] private float maxValue = 1f;
    [SerializeField] private float minValue = 0.2f;
    public float increaseFactor = 0.5f;
    public float minNeedleValue = 90f;
    public float maxNeedleValue = 90f;

    void Update()
    {
        heatAmount = currentHeatAmount;
        UpdateGauge();
    }

    private void UpdateGauge()
    {
        heatAmount = Mathf.Clamp(heatAmount, minValue, maxValue);
        SetNeedle();
    }

    private void SetNeedle()
    {
        needle.transform.localEulerAngles = new Vector3(0, 0, (heatAmount / increaseFactor * minNeedleValue - maxNeedleValue) * 1f);
    }
}
