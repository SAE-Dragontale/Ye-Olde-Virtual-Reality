using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusOne : MonoBehaviour {

    public FireCatapult powerMinusOne;



    void OnCollisionEnter(Collision collision)
    {
        powerMinusOne.power -= 1.0f;
    }
}
