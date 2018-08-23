using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TwoHandedItem : BasicPhysicsItem {


	List<GameObject> _handsUsed = new List<GameObject>();


	//Define Functionality for when an interactable object has focus and the trigger button is pressed/held
	public override void OnObjectInteractHold(GameObject hand, Animator anim, Transform grabPoint)
	{
		anim.SetBool ("CloseHand", true);

		_handsUsed.Add (hand.gameObject);
		_handsUsed = _handsUsed.Distinct ().ToList ();
		string handName = "";
		foreach(GameObject handy in _handsUsed)
		{
			handName += handy.name;
		}

		Debug.Log ("Handname is " + handName.ToString ());
		if(handName == "HandLeftHandRight" || handName == "HandRightHandLeft")
		{

			gripPoint.transform.position = _handsUsed [0].GetComponent<Hand> ().ReturnGrabPoint ();
			//	Vector3.Lerp(_handsUsed[0].transform.position, _handsUsed[1].transform.position, .5f);
			gripPoint.transform.rotation = grabPoint.transform.rotation;
			gripPoint.transform.SetParent (hand.transform);
			gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			_isInteractedWith = true;
			hand.GetComponent<Hand> ().SetJoint (gripPoint.GetComponent<Rigidbody>());

		}
	}

	//Define Functionality for when an interactable object has focus and the trigger button is released
	public override void OnObjectInteractRelease(GameObject hand, Animator anim)
	{
		anim.SetBool ("CloseHand", false);
		Hand[] hands = FindObjectsOfType<Hand> ();
		if(_handsUsed.Count == 2)
		{
			foreach (Hand _hand in hands) 
			{
				_hand.SetJoint (null);
			}

		}
		else
		{
			hand.GetComponent<Hand> ().SetJoint (null);
		}
		_handsUsed.Clear ();

		gripPoint.transform.parent = null;

		gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		_isInteractedWith = false;


		//Needs vive to test
		ThrowItem (hand.GetComponent<Hand> ().ReturnTrackedObj ());

	}

	public override void ThrowItem(SteamVR_TrackedObject trackedObj)
	{
		base.ThrowItem (trackedObj);
	}



}
