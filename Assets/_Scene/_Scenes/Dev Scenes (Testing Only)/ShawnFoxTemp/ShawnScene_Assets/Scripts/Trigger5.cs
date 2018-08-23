using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger5 : MonoBehaviour {
	
	public bool isTrigger5;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isTrigger5 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="PlayerWeapon")
		{
			isTrigger5 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.green;
		}
	}
}
