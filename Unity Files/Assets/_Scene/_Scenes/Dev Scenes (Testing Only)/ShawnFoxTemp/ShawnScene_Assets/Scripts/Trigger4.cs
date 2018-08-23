using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger4 : MonoBehaviour {

	public bool isTrigger4;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isTrigger4 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="PlayerWeapon")
		{
			isTrigger4 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.green;
		}
	}
}
