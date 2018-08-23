using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MagicTeleporter : BasicPhysicsItem {


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

			FindObjectOfType<AdditiveSceneMethod.AdditiveSceneController>().CallToArena();

		}
	}

//	//Define Functionality for when an interactable object has focus and the trigger button is released
//	public override void OnObjectInteractRelease(GameObject hand, Animator anim)
//	{
//		anim.SetBool ("CloseHand", false);
//		Hand[] hands = FindObjectsOfType<Hand> ();
//		if(_handsUsed.Count == 2)
//		{
//			foreach (Hand _hand in hands) 
//			{
//				_hand.SetJoint (null);
//			}
//
//		}
//		else
//		{
//			hand.GetComponent<Hand> ().SetJoint (null);
//		}
//		_handsUsed.Clear ();
//
//		gripPoint.transform.parent = null;
//
//		gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
//		_isInteractedWith = false;
//
//
//		//Needs vive to test
//		ThrowItem (hand.GetComponent<Hand> ().ReturnTrackedObj ());
//
//	}
//
//	public override void ThrowItem(SteamVR_TrackedObject trackedObj)
//	{
//		base.ThrowItem (trackedObj);
//	}
//


}
