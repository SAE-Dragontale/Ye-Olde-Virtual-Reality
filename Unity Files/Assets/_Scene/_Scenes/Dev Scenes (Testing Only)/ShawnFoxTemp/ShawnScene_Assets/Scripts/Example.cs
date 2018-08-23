using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

    float previousA;
    public float a;
    public float totalAmount;

    bool goingRight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      // GetComponent<Rigidbody>().AddTorque(Vector3.up * 10 * Time.deltaTime);


        if(previousA < a)
        {
            goingRight = true;

        }

        totalAmount += a - previousA;

        if (a < 0)
        a = 360+a;
  

        previousA = a;
        
	}
}
