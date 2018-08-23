using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger3 : MonoBehaviour {
	
	public bool isTrigger3;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isTrigger3 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="PlayerWeapon")
		{
			isTrigger3 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.green;
		}
	}
}
