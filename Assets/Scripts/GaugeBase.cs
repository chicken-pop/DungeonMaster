using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBase : MonoBehaviour
{
    private Image gauge => this.GetComponent<Image>();

    public void SetGauge(float HPRate)
    {
        gauge.fillAmount = HPRate;
    }
}
