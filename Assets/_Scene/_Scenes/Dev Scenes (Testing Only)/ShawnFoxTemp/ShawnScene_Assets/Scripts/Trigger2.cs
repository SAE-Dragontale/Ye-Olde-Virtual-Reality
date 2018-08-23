using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour {

	public bool isTrigger2;
	public GameObject Mug;

	// Use this for initialization
	void Start () 
	{
		isTrigger2 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="PlayerWeapon")
		{
			isTrigger2 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.green;
		}
	}

}
