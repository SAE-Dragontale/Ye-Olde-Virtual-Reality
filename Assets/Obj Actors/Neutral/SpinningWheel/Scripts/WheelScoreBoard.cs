using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelScoreBoard : MonoBehaviour {


	[SerializeField]
	DartsManager _dartsManager;

	[SerializeField]
	Text _hiScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void UpdateScoreboard(DartsManager manager)
	{
		_dartsManager = manager;
		_hiScore.text = manager.ReturnHighScore ().ToString ();

	}

}
