using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiTen : MonoBehaviour {

    public FireCatapult powermiTen;



    void OnCollisionEnter(Collision collision)
    {
        powermiTen.power -= 10.0f;
    }
}
