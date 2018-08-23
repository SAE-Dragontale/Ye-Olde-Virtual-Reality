using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minusOneZero : MonoBehaviour {

    public FireCatapult powerMinusPlusOne;



    void OnCollisionEnter(Collision collision)
    {
        powerMinusPlusOne.power -= 0.1f;
    }
}
