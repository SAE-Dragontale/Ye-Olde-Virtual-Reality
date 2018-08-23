using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatapultPowerUI : GenericUIWindowScript
{
    Text textObject;
    Image fillImage;
    [Range(0, 1)]
    float value;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        textObject = transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>();
        fillImage = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
    }

    public void SetPowerDisplay(float value)
    {
        value = (float)decimal.Round((decimal)value, 2);
        fillImage.fillAmount = value;
        textObject.text = (value * 100) + "%";
    }
}
