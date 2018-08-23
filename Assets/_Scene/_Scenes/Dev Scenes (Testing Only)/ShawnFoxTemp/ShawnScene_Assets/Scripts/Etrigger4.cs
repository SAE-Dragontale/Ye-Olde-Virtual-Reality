using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger4 : MonoBehaviour {
	
	public bool isETrigger4;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isETrigger4 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="EnemyWeapon")
		{
			isETrigger4 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
