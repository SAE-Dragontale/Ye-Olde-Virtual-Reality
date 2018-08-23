using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultHandler : MonoBehaviour
{
    [SerializeField]
    FireCatapult fc;
	// Use this for initialization
	void Start ()
    {
		
	}

    public void Fire()
    {
        fc.SendMessage("CreateProjectile");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
