using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatatuptBallTurnFinished : MonoBehaviour
{

    public bool Endplayersturn;
    public GameObject playerProjectile;


    void Awake()
    {
        Endplayersturn = false;
    }

    // Use this for initialization
    void Start ()
    {
        playerProjectile.GetComponent<GameObject>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Table") 
        {
            Endplayersturn = true;
            StartCoroutine(SetprojectileToFalse());
          
        }
        else if(collision.gameObject.tag == "GroundPlane")
        {
            Endplayersturn = true;
            StartCoroutine(SetprojectileToFalse());

        }
    }
    IEnumerator SetprojectileToFalse()
    {
        yield return new WaitForSeconds(12.0f);
        Endplayersturn = false;
    }
    }

