using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiCthulhuInteraction : BasicPhysicsItem {

	[SerializeField]
	AudioSource audioPlayer;


	void Awake()
	{
		audioPlayer = GetComponent<AudioSource> ();
	}

	public override void OnObjectInteractHold(GameObject hand, Animator anim, Transform grabPoint)
	{
		if(_isInteractedWith == false)
		{
			anim.SetBool ("CloseHand", true);

			gripPoint.transform.position = grabPoint.position;
			gripPoint.transform.SetParent (hand.transform);
			transform.localPosition = Vector3.zero;
			gripPoint.GetComponent<Animator> ().enabled = false;
			_isInteractedWith = true;
			gripPoint.transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y + 180, 90);
			gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			hand.GetComponent<Hand> ().SetJoint (gripPoint.GetComponent<Rigidbody>());
		}


	}

	public override void OnObjectInteractRelease(GameObject hand, Animator anim)
	{
		if(hand.GetComponent<Hand>().IsHoldingItem() == true)
		{
			hand.GetComponent<Hand> ().SetJoint (null);
			anim.SetBool ("CloseHand", false);
            gripPoint.transform.SetParent(this.transform.root.transform);

		
			gripPoint.transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y + 180, 90);
			gripPoint.GetComponent<Rigidbody> ().constraints &= ~RigidbodyConstraints.FreezePositionX;
			gripPoint.GetComponent<Rigidbody> ().constraints &= ~RigidbodyConstraints.FreezePositionY;
			gripPoint.GetComponent<Rigidbody> ().constraints &= ~RigidbodyConstraints.FreezePositionZ;
			gripPoint.GetComponent<Animator> ().enabled = true;
			_isInteractedWith = false;
		}



	}


	void OnTriggerEnter(Collider col)
	{
		if(col.GetComponent<InteractableItem>())
		{
			Debug.Log ("I was collided with");
			GetComponent<SphereCollider> ().enabled = false;
			gripPoint.GetComponent<Animator> ().SetTrigger ("Deflate");
			audioPlayer.Play ();
			StartCoroutine ("CallRespawn");
		}
	}

	IEnumerator CallRespawn()
	{
		yield return new WaitForSeconds (2);

		FindObjectOfType<OctoSpawner> ().CreateOctopus ();
		Hand[] hands = FindObjectsOfType<Hand> ();
		foreach(Hand hand in hands)
		{
			hand.RemoveFromItemList (this.gameObject);
		}
		Destroy (gripPoint.gameObject, Time.deltaTime);


	}

}
