using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{

	public Animator armAnim;

	// Use this for initialization
    void Start () {

		armAnim = GetComponent<Animator> ();
     

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            armAnim.Play("ArmDown");

        }
        else if (Input.GetMouseButtonUp(0))
        {

            armAnim.Play("LaunchArm");
        }
    }     

        

}

