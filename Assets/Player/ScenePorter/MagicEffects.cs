using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void Awake()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 60 * Time.deltaTime, 0);
	}
}
