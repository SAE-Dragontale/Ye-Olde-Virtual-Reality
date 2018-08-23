using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusZero : MonoBehaviour {

    public FireCatapult powerPlusZeroOne;



    void OnCollisionEnter(Collision collision)
    {
        powerPlusZeroOne.power += 0.1f;
    }
}
