using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyProjectileTurn : MonoBehaviour {

    public bool endEnermysturn;
    public GameObject enermyProjectile;

    void Awake()
    {
        endEnermysturn = false;
    }

    // Use this for initialization
    void Start()
    {
        enermyProjectile.GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Table")
        {
            endEnermysturn = true;
            StartCoroutine(SetEprojectileToFalse());
           
        }
        else if (collision.gameObject.tag == "GroundPlane")
        {
            endEnermysturn = true;
            StartCoroutine(SetEprojectileToFalse());

        }
    }
    IEnumerator SetEprojectileToFalse()
    {
        yield return new WaitForSeconds(12.0f);
        endEnermysturn = false;
    }
}
