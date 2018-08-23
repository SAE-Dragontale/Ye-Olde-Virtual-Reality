using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class BasicPhysicsItem : InteractableItem {




	/// <summary>
	/// Will hold an object if it is currently not being interacted with.
	/// </summary>
	/// <param name="hand">Hand.</param>
	/// <param name="anim">Animation.</param>
	/// <param name="grabPoint">Grab point.</param>
	public override void OnObjectInteractHold(GameObject hand, Animator anim, Transform grabPoint)
	{
		{
			anim.SetBool ("CloseHand", true);

			gripPoint.transform.position = grabPoint.position;
			gripPoint.transform.SetParent (hand.transform);
			_isInteractedWith = true;
			gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			hand.GetComponent<Hand> ().SetJoint (gripPoint.GetComponent<Rigidbody>());
		}


	}

	/// <summary>
	/// Will release an object if it is being held by the corresponding hand.
	/// Will inherit it's departure velocity from the VR controller velocity on release.
	/// </summary>
	/// <param name="hand">Hand.</param>
	/// <param name="anim">Animation.</param>
	public override void OnObjectInteractRelease(GameObject hand, Animator anim)
	{
		if(hand.GetComponent<Hand>().IsHoldingItem() == true)
		{
			hand.GetComponent<Hand> ().SetJoint (null);
			anim.SetBool ("CloseHand", false);
            gripPoint.transform.SetParent(this.transform.root.transform);

			gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			_isInteractedWith = false;


			//Needs vive to test
			ThrowItem (hand.GetComponent<Hand> ().ReturnTrackedObj ());
		}

	}


	/// <summary>
	/// If a controller is attached, this method will give a release object
	/// it's departure velocity.
	/// </summary>
	/// <param name="trackedObj">Tracked object.</param>
	public virtual void ThrowItem(SteamVR_TrackedObject trackedObj)
	{
		if(VRDevice.isPresent)
		{
            GameObject go = gripPoint;
			Rigidbody rigidbody = go.GetComponent<Rigidbody>();

			var device = SteamVR_Controller.Input((int)trackedObj.index);
			var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;

			if (origin != null)
			{
				rigidbody.velocity = origin.TransformVector(device.velocity);
				rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
			}
			else
			{
				rigidbody.velocity = device.velocity;
				rigidbody.angularVelocity = device.angularVelocity;
			}

			rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
		}

	}

}
