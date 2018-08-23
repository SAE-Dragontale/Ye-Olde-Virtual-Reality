using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(SteamVR_TrackedObject))]
[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{

	SteamVR_TrackedObject trackedObj;
	[SerializeField]
	Animator anim;
	[SerializeField]
	Transform _grabPoint;
	InteractableItem _currentItem;
	[SerializeField]
	FixedJoint joint;
	List<GameObject> _availableItems = new List<GameObject>();
	string _triggerAxisName;
	void Awake()
	{
		anim = GetComponent<Animator> ();
		if(VRDevice.isPresent)
		{
			Debug.Log("VR Found");
			trackedObj = GetComponent<SteamVR_TrackedObject>();
		}
		else
		{
			///Determines which mouse button effects which hand if a vr controller is not attached
			if(this.gameObject.name == "HandLeft")
			{
				_triggerAxisName = "TriggerLeft";
			}
			else
			{
				_triggerAxisName = "TriggerRight";
			}
			Debug.Log ("This object's name is " + this.gameObject.name);
		}

	}

	void Update()
	{
		if(VRDevice.isPresent)
		{
			var device = SteamVR_Controller.Input((int)trackedObj.index);
			if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))	{
				InteractWithObject ();
			}
			else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
			{
				InteractEnd ();
			}
		}
		else
		{
			if(Input.GetButtonDown(_triggerAxisName))
			{
				InteractWithObject ();
			}

			if(Input.GetButtonUp(_triggerAxisName))
			{
				InteractEnd ();
			}
		}
	}

	/// <summary>
	/// Calls the current interactable object's interact method, if the controller trigger is pressed
	/// </summary>
	void InteractWithObject()
	{
		if(_currentItem != null)
		{
			_currentItem.OnObjectInteractHold (this.gameObject, anim, _grabPoint);
		}
		else
		{
			anim.SetBool ("CloseHand", true);
			//Make the fist have collisions
			GetComponent<Collider> ().isTrigger = false;
		}
	
	}


	/// <summary>
	/// Calls the current interactable object's release method, if the controller trigger is released
	/// </summary>
	void InteractEnd()
	{
		if(joint.connectedBody != null)
		{
			joint.connectedBody.gameObject.GetComponentInChildren<InteractableItem>().OnObjectInteractRelease (this.gameObject, anim);

		}
		if(_currentItem != null)
		{
			if(_currentItem.gameObject.GetComponent<TwoHandedItem>())
			{
				_currentItem.OnObjectInteractRelease (this.gameObject, anim);
			}
		}
		else
		{
			anim.SetBool ("CloseHand", false);
			//Remove collisions if back to an open hand
			GetComponent<Collider> ().isTrigger = true;
		}

	}

	/// <summary>
	/// Adds an item that's entered the hand's focus trigger to a list of game objects
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.GetComponent<InteractableItem>())
		{
			_availableItems.Add (col.gameObject);
		//	Debug.Log ("collider added to list " + col.name + "Length is " + _availableItems.Count);
		}
	}


	/// <summary>
	/// Determines the closest object as the item to interact with.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerStay(Collider col)
	{
		if(!col.GetComponent<InteractableItem>())
		{
			return;
		}
		anim.SetBool ("Reach", true);
		if(_availableItems.Count > 0 && _availableItems.Count < 2)
		{
			_currentItem = _availableItems [0].GetComponent<InteractableItem> ();
			return;
		}
		if(_availableItems.Count > 1)
		{
			GameObject[] interactables = _availableItems.ToArray ();
			List<float> distance = new List<float> ();
			foreach(GameObject obj in interactables)
			{
				
				distance.Add (Vector3.SqrMagnitude (transform.position - obj.transform.position));
			}
			float[] dist = distance.ToArray ();

			int minIndex = System.Array.IndexOf (dist, distance.Min ());
			_currentItem = interactables [minIndex].GetComponent<InteractableItem>();

		}
	}

	/// <summary>
	/// Removes a game object from the list if it is no longer within the hand's range of focus
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerExit(Collider col)
	{

		_currentItem = null;
		_availableItems.Remove (col.gameObject);
		if(col.gameObject.GetComponent<TwoHandedItem>() && joint.connectedBody == col.transform.root.gameObject.GetComponent<Rigidbody>())
		{
			col.gameObject.GetComponent<TwoHandedItem> ().OnObjectInteractRelease (this.gameObject, anim);
		}
		if(_availableItems.Count > 0)
		{
			_currentItem = _availableItems [0].GetComponent<InteractableItem> ();
		}
		else
		{
			anim.SetBool ("Reach", false);
		}
	}

	/// <summary>
	/// Accessed by an object that can be picked up to set the connected body of the fixed joint
	/// </summary>
	/// <param name="rb">Rb.</param>
	public void SetJoint(Rigidbody rb)
	{
		joint.connectedBody = rb;
	}

	/// <summary>
	/// Returns the tracked object, required by objects that are able to be thrown
	/// </summary>
	/// <returns>The tracked object.</returns>
	public SteamVR_TrackedObject ReturnTrackedObj()
	{
		
		return  trackedObj;
	}

	/// <summary>
	/// Returns true if the fixed joint is connected to a rigid body
	/// </summary>
	/// <returns><c>true</c> if this instance is holding item; otherwise, <c>false</c>.</returns>
	public bool IsHoldingItem()
	{
		if(joint.connectedBody != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public InteractableItem ReturnCurrentItem()
	{
		return _currentItem;
	}

	public Rigidbody ReturnJointBody()
	{
		return joint.connectedBody;
	}

	public void RemoveFromItemList(GameObject queryItem)
	{
		if(_availableItems.Contains(queryItem))
		{
			_availableItems.Remove (queryItem);
		}
	}


	public Vector3 ReturnGrabPoint()
	{
		return _grabPoint.position;
	}


	public void RemoveAllItems()
	{
		InteractEnd ();
		SetJoint (null);
		_currentItem = null;
		_availableItems.Clear ();
		Debug.Log ("Items cleared");
	}

}
