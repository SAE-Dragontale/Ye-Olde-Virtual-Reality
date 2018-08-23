using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger6 : MonoBehaviour {
	
	public bool isTrigger6;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isTrigger6 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="PlayerWeapon")
		{
			isTrigger6 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.green;
		}
	}
}
