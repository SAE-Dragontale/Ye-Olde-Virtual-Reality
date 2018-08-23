using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBelt : InteractableItem {

	[SerializeField]
	GameObject knifePrefab;


	//Define Functionality for when an interactable object has focus and the trigger button is pressed/held
	public override void OnObjectInteractHold(GameObject hand, Animator anim, Transform grabPoint)
	{
		GameObject _knife = Instantiate (knifePrefab);
		_knife.transform.position = hand.transform.position + new Vector3(0, .4f, 0);
		//_knife.GetComponentInChildren<ThrowingKnife> ().StartCoroutine ("DissolveKnife");
		Debug.Log ("I created a knife");

		//Knife ejects from knife rack
		//_knife.GetComponentInChildren<InteractableItem>().gripPoint.GetComponent<Rigidbody>().velocity = new Vector3 (0, 4, 0);
		//Knife is just grabbed into hand on interaction
		_knife.GetComponentInChildren<InteractableItem>().OnObjectInteractHold(hand, anim, grabPoint);

	}

	//Define Functionality for when an interactable object has focus and the trigger button is released
	public override void OnObjectInteractRelease(GameObject hand, Animator anim)
	{
		if(hand.GetComponent<Hand>().IsHoldingItem() == true && hand.GetComponent<Hand>().ReturnCurrentItem() != this)
		{
			Debug.Log ("I did something");
			hand.GetComponent<Hand> ().ReturnCurrentItem ().OnObjectInteractRelease (hand, anim);



		}
	}




}
