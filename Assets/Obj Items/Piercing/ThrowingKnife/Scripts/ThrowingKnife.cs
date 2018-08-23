using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife :  WeaponInteractTemplate{

	[SerializeField]
	public GameObject sharpPoint;
	[SerializeField]
	AudioSource audioPlayer;
	[SerializeField]
	Material[] mats;

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
			gripPoint.transform.rotation = grabPoint.transform.rotation;
			gripPoint.transform.SetParent (hand.transform);

			gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			_isInteractedWith = true;
			StopCoroutine ("DissolveKnife");

			if(gameObject.GetComponent<MeshRenderer> ().material.color.a < 1)
			{
				MeshRenderer mr = gameObject.GetComponent<MeshRenderer> ();
				mr.material = mats [0];
				gameObject.GetComponent<MeshRenderer> ().material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, 1);
			}
			hand.GetComponent<Hand> ().SetJoint (gripPoint.GetComponent<Rigidbody>());
		}


	}

	public override void OnObjectInteractRelease(GameObject hand, Animator anim)
	{
		if(hand.GetComponent<Hand>().IsHoldingItem() == true)
		{
			hand.GetComponent<Hand> ().SetJoint (null);
			anim.SetBool ("CloseHand", false);


			gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			gripPoint.GetComponent<Rigidbody> ().isKinematic = false;
			_isInteractedWith = false;
			StartCoroutine("DissolveKnife");

			//Needs vive to test
			ThrowItem (hand.GetComponent<Hand> ().ReturnTrackedObj ());

			Invoke ("CheckSpeed", Time.deltaTime);//Check me out in VR
			gripPoint.transform.SetParent(this.transform.root.transform);
		}

	}


	void CheckSpeed()
	{
		float _velocity = Vector3.Magnitude (gripPoint.GetComponent<Rigidbody> ().velocity);
		if(_velocity > 9)//Change velocity to test against 5 when tested in VR
		{
			Debug.Log ("I am faster than lightning");
			audioPlayer.Play ();
		}
		Debug.Log ("Velocity is " + _velocity.ToString ());
	}

	public IEnumerator DissolveKnife()
	{
		Debug.Log ("Dissolving was called");
		yield return new WaitForSeconds (10);
		MeshRenderer mr = gameObject.GetComponent<MeshRenderer> ();
		float percent = 1;
		mr.material = mats [1];
		while(percent > 0)
		{
			mr.material.color = new Color (mr.material.color.r, mr.material.color.g, mr.material.color.b, percent);
			percent -= Time.deltaTime;
			yield return null;
		}
		Hand[] hands = FindObjectsOfType<Hand> ();
		foreach(Hand _hand in hands)
		{
			_hand.RemoveFromItemList (this.gameObject);
		}
		Destroy (gripPoint.gameObject);
	}


}
