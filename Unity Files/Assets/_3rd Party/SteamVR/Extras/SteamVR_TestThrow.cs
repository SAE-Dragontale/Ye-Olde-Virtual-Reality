//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow : MonoBehaviour
{
    public GameObject prefab;
    public Rigidbody attachPoint;

    public SteamVR_TrackedObject controller;
    FixedJoint joint;

    private void OnTriggerStay(Collider other)
    {
        var device = SteamVR_Controller.Input((int)controller.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Grip))
        {
            if (other.tag == "InteractableObject")
            {
                prefab = other.gameObject;
            }
        }
    }

    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)controller.index);
        if (joint == null)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                RaycastHit hit;
            }
        }
        if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (!prefab)
            {
                return;
            }
            var go = prefab;
            go.transform.position = attachPoint.transform.position;
            joint = go.AddComponent<FixedJoint>();

            joint.connectedBody = attachPoint;
            Debug.LogError("Sent Pickedup Message");
            prefab.SendMessage("PickedUp", controller as object);

        }
        else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip))
        {
            joint.connectedBody = attachPoint;
            prefab.SendMessage("Dropped");

            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            Object.DestroyImmediate(joint);
            joint = null;
            prefab = null;
            //Object.Destroy(go, 15.0f);

            // We should probably apply the offset between trackedObj.transform.position
            // and device.transform.pos to insert into the physics sim at the correct
            // location, however, we would then want to predict ahead the visual representation
            // by the same amount we are predicting our render poses.

            var origin = controller.origin ? controller.origin : controller.transform.parent;
            if (origin != null)
            {
                rigidbody.velocity = origin.TransformVector(device.velocity);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
            }
            else
            {
                rigidbody.velocity = device.velocity;
                rigidbody.angularVelocity = device.angularVelocity;
            }

            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
        }
    }
}
