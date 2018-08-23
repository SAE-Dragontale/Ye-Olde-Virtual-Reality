using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour {

    public bool isTrigger1;
	public GameObject Mug;

	// Use this for initialization
	void Start ()
    {
        isTrigger1 = false;
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag =="PlayerWeapon")
        {
            isTrigger1 = true;
			Mug.GetComponent<Renderer> ().material.color = Color.green;
        }
    }

}
