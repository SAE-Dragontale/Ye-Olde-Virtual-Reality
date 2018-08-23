using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class TavernPortal : TeleportLocation {

	public override void Awake ()
	{
		if(VRDevice.isPresent)
		{
			InitializeOnTeleport (new Vector3 (0, 0, 0));
		}
		else
		{
			InitializeOnTeleport (new Vector3 (0, .98f, 0));
		}

		_mCircle.InTavern (true);
		_mCircle.SetDestination ("PortTestArena");
		Debug.Log ("Awake in Tavern");
	}
}
