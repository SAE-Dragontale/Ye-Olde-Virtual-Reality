using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger3 : MonoBehaviour {
	
	public bool isETrigger3;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isETrigger3 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="EnemyWeapon")
		{
			isETrigger3 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
