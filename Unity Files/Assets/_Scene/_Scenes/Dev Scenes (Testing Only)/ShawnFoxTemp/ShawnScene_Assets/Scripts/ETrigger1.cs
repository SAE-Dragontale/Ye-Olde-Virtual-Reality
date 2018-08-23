using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETrigger1 : MonoBehaviour {
	
	public bool isETrigger1;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
	{
		isETrigger1 = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag =="EnemyWeapon")
		{
			isETrigger1 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
