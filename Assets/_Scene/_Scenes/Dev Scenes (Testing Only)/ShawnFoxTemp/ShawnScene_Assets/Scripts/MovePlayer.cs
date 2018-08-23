using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;

public class MovePlayer : MonoBehaviour
{
	public SteamVR_TrackedObject controller;
	public SpriteRenderer teleportReticle;
	public Sprite canTeleportSprite;
	public Sprite noTeleportSprite;
	public Color colorRed;
	public Color colorGreen;
	public LayerMask colliderLayerMask;
	public Image img;
	public bool doFade;
	public bool isFading;
	public Vector3 newPos;

	public Material redLaser;
	public Material greenLaser;
	public MeshRenderer laserPointer;

	// Use this for initialization
	void Start()
	{

	}

	private void FixedUpdate()
	{
		if (!VRDevice.isPresent)
		{
			return;
		}
		var device = SteamVR_Controller.Input((int)controller.index);

		if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
		{
			RaycastHit hit;
			if (Physics.Raycast(controller.gameObject.transform.position, controller.transform.forward, out hit, 999, colliderLayerMask))
			{
				if (teleportReticle.enabled == false)
				{
					teleportReticle.enabled = true;
				}
				teleportReticle.transform.position = hit.point;
				teleportReticle.transform.position = new Vector3(teleportReticle.transform.position.x, 0.01f, teleportReticle.transform.position.z);
				if (hit.collider.tag == "CanTeleport")
				{
					teleportReticle.sprite = canTeleportSprite;
					teleportReticle.color = colorGreen;
					laserPointer.enabled = true;
					laserPointer.material = greenLaser;
					if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
					{
						Teleport(hit.point);
					}
				}
				else
				{
					teleportReticle.sprite = noTeleportSprite;
					teleportReticle.color = colorRed;
					laserPointer.enabled = true;
					laserPointer.material = redLaser;
				}
			}
			else
			{
				if (teleportReticle.enabled == true)
				{
					teleportReticle.enabled = false;
					laserPointer.enabled = false;
				}
			}
		}
		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			teleportReticle.enabled = false;
			laserPointer.enabled = false;
		}
	}

	void Teleport(Vector3 newPosition)
	{
		doFade = true;
		isFading = false;
		newPos = newPosition;
		Invoke("DoTeleport", 0.8f);
		Invoke("FadeIn", 1f);
	}

	void DoTeleport()
	{
		transform.position = newPos;
		transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
	}

	void FadeIn()
	{
		isFading = true;
		doFade = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (doFade)
		{
			if (isFading)
			{
				if (img.color.a > 0)
				{
					img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - Time.deltaTime * 2);
				}
				else
				{
					doFade = false;
				}
			}
			else
			{
				if (img.color.a < 1)
				{
					img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + Time.deltaTime * 2);
				}
				else
				{
					doFade = false;
				}
			}
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += new Vector3(0, 0, -1) * Time.deltaTime;
		}
	}
}
