using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOne : MonoBehaviour {

    public FireCatapult powerPlusOne;



    void OnCollisionEnter(Collision collision)
    {
        powerPlusOne.power += 1.0f;
    }
}
