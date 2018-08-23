using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteractor : MonoBehaviour
{
	public SteamVR_TrackedObject controller;
	public CatapultHandler cataHandler;
	// Use this for initialization
	void Start()
	{
        cataHandler = GameObject.Find("[CD]Player").GetComponent<CatapultHandler>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)controller.index);

		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			RaycastHit hit;
			if (Physics.Raycast(controller.gameObject.transform.position, controller.transform.forward, out hit, 999))
			{
				if (hit.collider.gameObject.name.Contains("[CD]"))
				{
					cataHandler.Fire();
				}
			}
		}
	}

}
