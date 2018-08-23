using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEditor;

public class FireProjectile : MonoBehaviour
{
	public SteamVR_TrackedObject controller;
	public bool pickedUp = false;
	public GameObject arrowObj;
	public GameObject projectile;
	public float firePower;
	public GameObject firePoint;

	// Use this for initialization
	void Start()
	{

	}

	void PickedUp()
	{
		pickedUp = true;
	}

	void Dropped()
	{
		pickedUp = false;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)controller.index);

		if (pickedUp)
		{
			if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
			{
				firePower += device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
			}
			else
			{
				arrowObj.transform.Rotate(0, 0, device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x);
			}
		}


	}

	public void Fire()
	{
		GameObject tempObject;
		tempObject = Instantiate (projectile.gameObject, firePoint.transform) as GameObject;
		tempObject.transform.localScale = new Vector3 (0.2f, 2.1f, 2.1f);
		tempObject.GetComponent<Rigidbody> ().AddForce (-firePoint.transform.right * firePower, ForceMode.VelocityChange);
		tempObject = null;
		EditorApplication.isPaused = true;
	}
}
