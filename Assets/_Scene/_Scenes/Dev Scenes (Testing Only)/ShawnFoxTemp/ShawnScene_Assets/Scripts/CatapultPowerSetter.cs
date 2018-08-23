using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultPowerSetter : MonoBehaviour
{
    [SerializeField]
    FireCatapult fireCatapult;
    [Header("Autos")]

    [SerializeField]
    CatapultPowerUI powerUI;
    bool oldPowerValue;
    public GameObject lever;

    // Use this for initialization
    void Start()
    {
        
    }

    public void ChangeLeverRotation(float value)
    {
        lever.transform.Rotate(new Vector3(value, 0, 0));
        fireCatapult.power += value;
        fireCatapult.power = Mathf.Clamp(fireCatapult.power, 0, 100);
        powerUI.SetPowerDisplay(fireCatapult.power * 0.01f);
    }

    public void TogglePowerUI(bool value)
    {
        if (oldPowerValue == value)
        {
            return;
        }
        if (value)
        {
            powerUI.Show();
        }
        else
        {
            powerUI.Hide();
        }
        oldPowerValue = value;
    }

    public void FireCatapult()
    {
        fireCatapult.Fire();
    }
}
