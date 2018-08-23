using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class WheelQuadrant : MonoBehaviour {



	private AudioSource _audioPlayer;
	Hand[] hands;
	DartsManager manager;

	void Awake()
	{
		manager = FindObjectOfType<DartsManager> ();
	}

	// Use this for initialization
	void Start () {

		hands = FindObjectsOfType<Hand> ();
		if(_audioPlayer == null)
		{
			_audioPlayer = GetComponent<AudioSource> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col)
	{
		
		if(ReturnWheelQuadrant() == manager.ReturnTargetQuadrant() && manager.IsPlaying() == true)
		{
			_audioPlayer.Play ();
			manager.PlayerWins ();
		}
		else
		{
			manager.PlayerLoses ();
		}
		if(col.gameObject.GetComponentInChildren<ThrowingKnife>())
		{
			col.gameObject.GetComponentInChildren<ThrowingKnife> ().transform.SetParent (col.gameObject.GetComponentInChildren<ThrowingKnife> ().sharpPoint.transform);

			col.gameObject.GetComponentInChildren<ThrowingKnife> ().sharpPoint.transform.position = col.contacts [0].point;
			col.gameObject.GetComponentInChildren<ThrowingKnife> ().transform.SetParent (col.gameObject.GetComponentInChildren<ThrowingKnife> ().gripPoint.transform);
			col.transform.SetParent (transform.parent.transform);
		//	col.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			col.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			col.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			col.gameObject.transform.localRotation = Quaternion.Euler (col.gameObject.transform.localRotation.x, col.gameObject.transform.localRotation.y, 90);
			col.gameObject.GetComponentInChildren<ThrowingKnife> ().StartCoroutine ("DissolveKnife");

			Hand[] hands = FindObjectsOfType<Hand> ();
			foreach(Hand _hand in hands)
			{
				if(_hand.ReturnJointBody() == col.gameObject.GetComponent<Rigidbody>())
				{
					_hand.SetJoint (null);
				}
			}
		}
	}

	public virtual int ReturnWheelQuadrant ()
	{
		return -2;
	}

}
