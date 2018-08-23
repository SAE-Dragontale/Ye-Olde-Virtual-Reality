using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour {

	[SerializeField]
	GameObject player;

	// Use this for initialization
	void Start () {
		AssignPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		//player = GameObject.FindGameObjectWithTag ("Player");
	//	transform.LookAt (new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));

	}

	void AssignPlayer()
	{
	//	player = FindObjectOfType<RigAssignment> ().ReturnActivePlayer ();
	}

}
