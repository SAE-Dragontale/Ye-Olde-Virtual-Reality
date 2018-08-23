using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCatapult : MonoBehaviour
{
    [SerializeField]
    TeleportVive tv;
    [SerializeField]
    SteamVR_TrackedObject controller;
    CatapultPowerSetter powerSetter;
    public bool catapultOnly;
	// Use this for initialization
	void Start ()
    {
        if (!catapultOnly)
        {
            controller = GetComponent<SteamVR_TrackedObject>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!catapultOnly)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)controller.index);
            if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit))
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.name == "Player")
                    {
                        if (!powerSetter)
                        {
                            powerSetter = hit.collider.gameObject.GetComponent<CatapultPowerSetter>();
                        }
                        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                        {
                            device.TriggerHapticPulse(500);
                            FireCatapult();
                        }
                        else
                        {
                            powerSetter = hit.collider.gameObject.GetComponent<CatapultPowerSetter>();
                            powerSetter.TogglePowerUI(true);

                            float value = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;

                            powerSetter.ChangeLeverRotation(value);
                        }
                    }
                    else
                    {
                        if (powerSetter)
                        {
                            powerSetter.TogglePowerUI(false);
                        }
                    }
                }
            }
            else
            {
                if (powerSetter)
                {
                    powerSetter.TogglePowerUI(false);
                }
            }
        }
	}

    void FireCatapult()
    {
        if (powerSetter)
        {
            powerSetter.FireCatapult();
        }
    }
}
