using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour {

	[SerializeField]
	DartsManager _manager;
	[SerializeField]
	bool _isInside;
	// Use this for initialization
	void Start () {
		_manager = FindObjectOfType<DartsManager> ();
		_isInside = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("someone is in me" + col.transform.root.name);
		if(col.gameObject.GetComponent<Hand>() && !_isInside)
		{
			Debug.Log ("A player is in me");
		//	_manager.BeginGame ();
			_isInside = true;
			_manager.ActivateScoreBubble ();
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.GetComponent<Hand>() && _isInside)
		{
			_manager.EndGame ();
			_isInside = false;
		}
	}

	public bool ReturnInside()
	{
		return _isInside;
	}

}
