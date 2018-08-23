using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger2 : MonoBehaviour {
	
	public bool isETrigger2;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isETrigger2 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="EnemyWeapon")
		{
			isETrigger2 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
