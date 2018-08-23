using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class FireCatapult : BasicPhysicsItem
{
    [SerializeField] private GameObject _levelParent;
    public GameObject projectlePrefab;
    [Header("Autos")]
    public Animator armAnim;
    public GameObject projectile;

    [Range(0, 100)] public float power;

    private float angle = 90f;
    public Rigidbody rb;
    public Transform startPosition;
    public TurnBasedStateMachine _MTBM;

    public bool isPressed, isThrown, isStopped, isBall;
    // Use this for initialization
    void Start()
    {
        armAnim = transform.parent.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        isPressed = isThrown = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            armAnim.Play("ArmDown");

        }
        else if (Input.GetMouseButtonUp(0))
        {
            armAnim.Play("LaunchArm");
        }


        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if (!isBall)
            {

            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            CreateProjectile();
            LaunchProjectile();
        }
    }

    public void Fire()
    {
        CreateProjectile();
    }

    private void LaunchProjectile()
    {
        Vector3 vForce = transform.forward * power + transform.up * power;
        Debug.Log("RUnningh?");
        rb.AddForce(vForce);
    }


    void CreateProjectile()
    {
        projectile = Instantiate(projectlePrefab, startPosition.position, Quaternion.Euler(0f, 0f, 45f)) as GameObject;
        projectile.transform.parent = _levelParent.transform;
        rb = projectile.GetComponent<Rigidbody>();
        Vector3 pos = startPosition.position;
        projectile.transform.position = pos;
        projectile.SetActive(true);
        LaunchProjectile();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I hit a Cup");
        isStopped = true;
        isThrown = true;
    }

    //public override void OnObjectInteractHold(GameObject hand, Animator anim, Transform grabPoint)
    //{
    //    armAnim.Play("ArmDown");
    //    if (_isInteractedWith == false)
    //    {
    //        anim.SetBool("CloseHand", true);
    //        gripPoint.transform.position = grabPoint.position;
    //        gripPoint.transform.rotation = grabPoint.transform.rotation;
    //        gripPoint.transform.SetParent(hand.transform);

    //        gripPoint.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //        _isInteractedWith = true;
    //        hand.GetComponent<Hand>().SetJoint(gripPoint.GetComponent<Rigidbody>());
    //    }
    //}

    //public override void OnObjectInteractRelease(GameObject hand, Animator anim)
    //{
    //    base.OnObjectInteractRelease(hand, anim);
    //    armAnim.Play("LaunchArm");
    //    CreateProjectile();
    //    LaunchProjectile();
    //}


}
