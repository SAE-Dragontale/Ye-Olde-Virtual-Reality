using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusTenForPower : MonoBehaviour {

    public FireCatapult powerPlusTen;

	

    void OnCollisionEnter(Collision collision)
    {
        powerPlusTen.power += 10.0f;
    }
}
