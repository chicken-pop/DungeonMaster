using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHPGauge : GaugeBase
{
   public CharacterParameterBase CharacterParameter;

    private float hpRate = 0f;

    private RectTransform gaugeRectTransform;

    private void Start()
    {
        hpRate = CharacterParameter.GetHitPoint / CharacterParameter.GetMaxHitPoint;
        SetGauge(hpRate);
        gaugeRectTransform = this.transform.parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        gaugeRectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,
            CharacterParameter.gameObject.transform.position + new Vector3(0,1));

        if(hpRate == CharacterParameter.GetHitPoint / CharacterParameter.GetMaxHitPoint)
        {
            return;
        }
        hpRate = CharacterParameter.GetHitPoint / CharacterParameter.GetMaxHitPoint;
        SetGauge(hpRate);
    }
}
