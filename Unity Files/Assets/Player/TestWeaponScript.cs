using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeaponScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void Interact(Transform inParent)
    {
        gameObject.transform.position = inParent.position;
        gameObject.transform.rotation = inParent.rotation;

        gameObject.transform.SetParent(inParent);
    }
}
