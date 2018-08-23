using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICatapult : MonoBehaviour {

    public Transform[] hitTargets;

    public float angleMain = 45;

    public GameObject cannonball;

    public Vector3[] Offsets;

    public Vector3 newTarget;

    public Rigidbody rbE;

    public TurnBasedStateMachine TBM;

    public bool isReadyForNextAttack;

    bool hasFired;

    bool hasSelected;
	// Use this for initialization
	void Start ()
    {
        hasFired = true;
        hasSelected = true;
        Offsets = new Vector3[6];

        Offsets[0] = hitTargets[0].position ;
        Offsets[1] = hitTargets[1].position;
        Offsets[2] = hitTargets[2].position;
        Offsets[3] = hitTargets[3].position;
        Offsets[4] = hitTargets[4].position;
        Offsets[5] = hitTargets[5].position;


        TBM.GetComponent<TurnBasedStateMachine>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        LaunchProjectileAtTarget();
        /*
        if(Input.GetMouseButtonUp(0))
        {
            Selecttarget();
        }

        if(Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(cannonball, transform.position, Quaternion.identity) as GameObject;
           // rbE.AddForce(Ballisticvel(newTarget, angleMain));
            ball.GetComponent<Rigidbody>().velocity = Ballisticvel(newTarget , angleMain);

        }
        */


    }

     Vector3 Ballisticvel (Vector3 newTarget,float angle)
     {
         var dir = newTarget - transform.position; // gets target direction
         var h = dir.y;// get height difference
         dir.y = 0;//retain only the horizontal direction
         var dist = dir.magnitude; // get horizontal distance
         var a = angle * Mathf.Deg2Rad; // convert angle to radians
         dir.y = dist * Mathf.Tan(a); //set dir to the elevation angle
         dist += h / Mathf.Tan(a);//correct for small height differences
         //Calculate the velocity magnitude
         var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
         return vel * dir.normalized;

     }




    void Selecttarget()
    {
        int pickATarget = Random.Range(0, 7);
        Debug.Log(pickATarget);

        switch (pickATarget)
        {
            case 0:
                Offsets[pickATarget] = hitTargets[pickATarget].transform.position + new Vector3(Random.Range(0.0f,0.3f),0.0f, Random.Range(0.0f, 0.3f));
                newTarget = Offsets[pickATarget];
                break;

            case 1:
                Offsets[pickATarget] = hitTargets[pickATarget].transform.position + new Vector3(Random.Range(0.0f, 0.3f), 0.0f, Random.Range(0.0f, 0.3f));
                newTarget = Offsets[pickATarget];
                break;

            case 2:
                Offsets[pickATarget] = hitTargets[pickATarget].transform.position + new Vector3(Random.Range(0.0f, 0.3f), 0.0f, Random.Range(0.0f, 0.3f));
                newTarget = Offsets[pickATarget];
                break;
            case 3:
                Offsets[pickATarget] = hitTargets[pickATarget].transform.position + new Vector3(Random.Range(0.0f, 0.3f), 0.0f, Random.Range(0.0f, 0.3f));
                newTarget = Offsets[pickATarget];
                break;

            case 4:
                Offsets[pickATarget] = hitTargets[pickATarget].transform.position + new Vector3(Random.Range(0.0f, 0.3f), 0.0f, Random.Range(0.0f, 0.3f));
                newTarget = Offsets[pickATarget];
                break;
                
            case 5:
                Offsets[pickATarget] = hitTargets[pickATarget].transform.position + new Vector3(Random.Range(0.0f, 0.3f), 0.0f, Random.Range(0.0f, 0.3f));
                newTarget = Offsets[pickATarget];
                break;

            default:
                break;
        }
       
   
    }

    
    

    void AttackPlayer()
    {
       
        GameObject ball = Instantiate(cannonball, transform.position, Quaternion.identity) as GameObject;
        // rbE.AddForce(Ballisticvel(newTarget, angleMain));
        ball.GetComponent<Rigidbody>().velocity = Ballisticvel(newTarget, angleMain);
      
    }

    void LaunchProjectileAtTarget()
    {
        //Debug.Log("TBM.IsenermyTurn " + TBM.GetComponent<TurnBasedStateMachine>().IsenermyTurn);
        if(TBM.IsenermyTurn == true)
        {
            Selecttarget();
            isReadyForNextAttack = true;
            hasFired = true;

        }
        else if(isReadyForNextAttack)
        {
            if (hasFired == true)
            { 
                AttackPlayer();
                hasFired = false;
                
            }
        }
       
    }
}


    

