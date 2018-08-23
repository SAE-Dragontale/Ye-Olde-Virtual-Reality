
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.VR;

public class TeleportLocation : MonoBehaviour {

	public MagicCircle _mCircle;
	[SerializeField]
	GameObject _ActivePlayer;
	PostProcessingProfile _postProfile;

	[SerializeField]
	GameObject[] playerRigs;


	// Use this for initialization
	public virtual void Awake () {
		
		InitializeOnTeleport (new Vector3(-15, 0, 0));



	}

	public void InitializeOnTeleport(Vector3 spawnPoint)
	{
		
		_mCircle = FindObjectOfType<MagicCircle> ();
		Debug.Log ("Mcircle is " + _mCircle);

		if(VRDevice.isPresent)
		{
			_ActivePlayer = playerRigs [1];
		}
		else
		{
			_ActivePlayer = playerRigs [0];
		}

		_ActivePlayer.transform.SetParent (null);

		if(_mCircle == null)
		{
			_ActivePlayer.SetActive (true);
			_mCircle = _ActivePlayer.GetComponentInChildren<MagicCircle> ();

			Debug.Log ("Activated default player");
		}
		else
		{
			Debug.Log ("I am reaching else");
			Debug.Log ("Coordinates are " + spawnPoint.ToString ());
			if(_mCircle.ReturnTeleportStatus() == false)
			{
	

				StartCoroutine ("SceneLoaded", spawnPoint);
				Debug.Log ("New scene loaded");
				_mCircle.StopParticles ();
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SceneLoaded(Vector3 spawnPoint)
	{
		Camera.main.transform.root.transform.position = new Vector3 (spawnPoint.x, spawnPoint.y, spawnPoint.z);
		Camera.main.transform.root.transform.root.rotation = Quaternion.Euler (0, 0, 0);
		Camera.main.transform.localRotation = Quaternion.Euler (0, 0, 0);

		//Remove all items from hands
		Hand[] hands = FindObjectsOfType<Hand> ();
		foreach(Hand hand in hands)
		{
			hand.RemoveAllItems ();
		}

		if(Camera.main.GetComponent<PostProcessingBehaviour> ())
		{
			_postProfile = Camera.main.GetComponent<PostProcessingBehaviour> ().profile;
		}
		if(_postProfile != null)
		{
			StartCoroutine ("RestoreBloom");
		}

		yield return null;
		_mCircle.FinishedTeleporting ();

	}



	IEnumerator RestoreBloom()
	{
		var _bloom = _postProfile.bloom.settings;
		while(_bloom.bloom.intensity > 0)
		{
			_bloom.bloom.intensity -= Time.deltaTime * 5;
			_postProfile.bloom.settings = _bloom;
			Debug.Log ("bloom intensity next" + _bloom.bloom.intensity.ToString ());
			if(_bloom.bloom.intensity < 0)
			{
				_bloom.bloom.intensity = 0;
				_postProfile.bloom.settings = _bloom;
				Debug.Log ("Bloom restored to zero");
				break;
			}
			yield return null;
		}
	}


	public GameObject ReturnActivePlayer()
	{
		return _ActivePlayer;
	}

}
