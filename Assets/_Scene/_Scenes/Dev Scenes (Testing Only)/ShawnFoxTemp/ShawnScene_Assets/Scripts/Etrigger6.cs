using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etrigger6 : MonoBehaviour {
	
	public bool isETrigger6;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isETrigger6 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="EnemyWeapon")
		{
			isETrigger6 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
