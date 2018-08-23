using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CausticLasagne.VR
{
public class GetControllerButtons : MonoBehaviour
{
    public SteamVR_TrackedObject controllerL;
    public SteamVR_TrackedObject controllerR;
	public bool autoEnabledDiag;
    public bool enabledDiag;
    public Text textObj;
    SteamVR_Controller.Device device;
    SteamVR_Controller.Device device2;
	public float enableTime = 1;

    // Use this for initialization
    void Start()
    {
		if (autoEnabledDiag)
		{
			Invoke ("DelayedEnable", enableTime);
		}

        if (SteamVR_Controller.Input((int)controllerL.index) == null)
        {
            Debug.LogError("Left controller was not found.");
        }

        if (SteamVR_Controller.Input((int)controllerR.index) == null)
        {
            Debug.LogError("Right controller was not found.");
        }
        if (SteamVR_Controller.Input((int)controllerL.index) == null || SteamVR_Controller.Input((int)controllerR.index) == null)
        {
            Debug.LogError("Diagnostics System Error: Two or more controllers were not found.");
        }
    }

	void DelayedEnable()
	{
		enabledDiag = true;
	}

    void ResetText()
    {
        textObj.color = Color.black;
        textObj.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            try
            {
                if (device.connected && device2.connected)
                {
                    enabledDiag = !enabledDiag;
                    if (!enabledDiag)
                    {
                        textObj.text = "";
                    }
                }
                else
                {
                    textObj.color = Color.red;
                    textObj.text = "One or more controllers are disconnected. Please connect at least 2 controllers to enable Debug.";
                    Invoke("ResetText", 1);
                }
            }
            catch (System.Exception ea)
            {
                textObj.color = Color.red;
                textObj.text = "Error getting controllers. Please connect at least 2 controllers to enable Debug.";
                CancelInvoke();
                Invoke("ResetText", 3);
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (enabledDiag)
        {
            textObj.text = "";
            string outDiag = "";
            device = SteamVR_Controller.Input((int)controllerL.index);
            device2 = SteamVR_Controller.Input((int)controllerR.index);

            if (device.connected)
            {
                if (!device.outOfRange)
                {
                    outDiag += ("\nController Left");
					// Left Application menu button
                    outDiag += ("ControllerL.AppMenu=" + device.GetTouch(SteamVR_Controller.ButtonMask.ApplicationMenu));

					// Left Touch pad Axis. X for hor, Y for ver
                    outDiag += ("\nControllerL.Axis0=" + device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));
					// Trigger Touch value. X only value.
					outDiag += ("\nControllerL.Axis1=" + device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1));
					// Nothing
					outDiag += ("\nControllerL.Axis2=" + device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2));
					// Grip button press
					outDiag += ("\nControllerL.Grip=" + device.GetTouch(SteamVR_Controller.ButtonMask.Grip));
					// System press. Doesn't seem to interact with game.
					outDiag += ("\nControllerL.System=" + device.GetTouch(SteamVR_Controller.ButtonMask.System));

					// Trigger touch. Value becomes true at about 0.25 - 0.3 of axis
					outDiag += ("\nControllerL.TriggerTouch=" + device.GetTouch(SteamVR_Controller.ButtonMask.Trigger));
					// Trigger press. Value becomes true at about 0.55 - 0.6 of axis
					outDiag += ("\nControllerL.TriggerPress=" + device.GetPress(SteamVR_Controller.ButtonMask.Trigger));

					// Triggered on touchpad TOUCH
					outDiag += ("\nControllerL.TouchpadTouch=" + device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad));
					// Triggered on touchpad PRESS
					outDiag += ("\nControllerL.TouchpadPress=" + device.GetPress(SteamVR_Controller.ButtonMask.Touchpad));
                }
                else
                {
                    outDiag += ("WARN! Controller Left Out of Range!");
                }
            }
            else
            {
				outDiag += ("WARN! Controller Left Disconnected!");
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (device.connected)
            {
                if (!device.outOfRange)
                {
                    outDiag += ("\n\nController Right");
                    outDiag += ("ControllerR.AppMenu=" + device2.GetTouch(SteamVR_Controller.ButtonMask.ApplicationMenu));

					outDiag += ("\nControllerR.Axis0=" + device2.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));
					outDiag += ("\nControllerR.Axis1=" + device2.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1));
					outDiag += ("\nControllerR.Axis2=" + device2.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis2));

					outDiag += ("\nControllerR.Grip=" + device2.GetTouch(SteamVR_Controller.ButtonMask.Grip));
					outDiag += ("\nControllerR.System=" + device2.GetTouch(SteamVR_Controller.ButtonMask.System));

					outDiag += ("\nControllerR.TriggerTouch=" + device2.GetTouch(SteamVR_Controller.ButtonMask.Trigger));
					outDiag += ("\nControllerR.TriggerPress=" + device2.GetPress(SteamVR_Controller.ButtonMask.Trigger));

					outDiag += ("\nControllerR.TouchpadTouch=" + device2.GetTouch(SteamVR_Controller.ButtonMask.Touchpad));
					outDiag += ("\nControllerR.TouchpadPress=" + device2.GetPress(SteamVR_Controller.ButtonMask.Touchpad));
                }
                else
                {
					outDiag += ("\n\nWARN! Controller Right Out of Range!");
                }
            }
            else
            {
				outDiag += ("\n\nWARN! Controller Right Disconnected!");
            }

			textObj.text = outDiag;

        }
    }
}
}