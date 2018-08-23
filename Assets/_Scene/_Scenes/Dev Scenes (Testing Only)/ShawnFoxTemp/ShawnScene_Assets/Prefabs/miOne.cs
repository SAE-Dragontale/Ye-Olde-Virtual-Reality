using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miOnes : MonoBehaviour {

    public FireCatapult powerMiOne;



    void OnCollisionEnter(Collision collision)
    {
        powerMiOne.power += 1.0f;
    }
}
