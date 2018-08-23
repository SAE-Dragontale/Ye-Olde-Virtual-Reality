using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger5 : MonoBehaviour {
	
	public bool isETrigger5;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isETrigger5 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="EnemyWeapon")
		{
			isETrigger5 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
