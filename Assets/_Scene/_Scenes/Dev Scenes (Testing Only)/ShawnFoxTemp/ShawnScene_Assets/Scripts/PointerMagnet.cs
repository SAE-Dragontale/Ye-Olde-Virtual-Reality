using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CausticLasagne.VR
{
    public class PointerMagnet : MonoBehaviour
    {
        public SteamVR_TrackedObject controller;
        public GameObject selectorUI;
        public Image[] controllerButton = new Image[4];
        public bool menuShown;
        public byte selectedOption;
        public Color[] colors = new Color[2];

        void Awake()
        {

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (SteamVR_Controller.Input((int)controller.index) == null)
            {
                return;
            }
            var device = SteamVR_Controller.Input((int)controller.index);

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                selectorUI.gameObject.SetActive(true);
                menuShown = true;
            }
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                selectorUI.gameObject.SetActive(false);
                menuShown = false;
            }
            if (menuShown)
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    if (selectedOption > controllerButton.Length - 2)
                    {
                        selectedOption = 0;
                    }
                    else
                    {
                        selectedOption++;
                    }

                    for (int i = 0; i < controllerButton.Length; i++)
                    {
                        if (i == selectedOption)
                        {
                            controllerButton[i].color = colors[1];
                        }
                        else
                        {
                            controllerButton[i].color = colors[0];
                        }
                    }
                }
            }
        }
    }
}