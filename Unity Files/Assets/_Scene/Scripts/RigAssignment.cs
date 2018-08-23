using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class RigAssignment : MonoBehaviour {


	[SerializeField]
	GameObject[] _rigs;
	[SerializeField]
	GameObject _activePlayer;
	// Use this for initialization
	void Awake () {

		if(Camera.main)
		{
			Debug.Log ("I found a camera");
			return;
		}

		if(VRDevice.isPresent)
		{
			_activePlayer = _rigs [1];
		}
		else
		{
			_activePlayer = _rigs [0];
			_activePlayer.transform.SetParent (null);
		}

		_activePlayer.SetActive (true);
	//	DontDestroyOnLoad (_activePlayer.gameObject);
		
	}
	

	public GameObject ReturnActivePlayer()
	{
		return _activePlayer;
	}

}
