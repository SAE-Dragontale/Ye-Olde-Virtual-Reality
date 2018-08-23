using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInteract : WeaponInteractTemplate
{
    WeaponScript _weaponScript;


    //Define Functionality for when an interactable object has focus and the trigger button is pressed/held
    public override void OnObjectInteractHold(GameObject hand, Animator anim, Transform grabPoint)
    {
        if(_isInteractedWith == false)
        {
            WeaponScript weaponScript = grabPoint.gameObject.GetComponent<WeaponScript>();

            anim.SetBool ("CloseHand", true);
            gripPoint.transform.position = grabPoint.position;

            //gripPoint.transform.forward = grabPoint.transform.forward;

            //gripPoint.transform.up = grabPoint.transform.right;
            gripPoint.transform.SetParent (hand.transform);
            gripPoint.transform.localRotation = Quaternion.Euler (0, -90, -90);
            //gripPoint.transform.up = grabPoint.transform.right;

            gripPoint.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
            _isInteractedWith = true;
            hand.GetComponent<Hand> ().SetJoint (gripPoint.GetComponent<Rigidbody>());

            _weaponScript = gripPoint.gameObject.GetComponent<WeaponScript>();
            _weaponScript.PlayerPickupAction();
        }
    }

    //Define Functionality for when an interactable object has focus and the trigger button is released
    public override void OnObjectInteractRelease(GameObject hand, Animator anim)
    {
        base.OnObjectInteractRelease (hand, anim);
    }

    public override void ThrowItem(SteamVR_TrackedObject trackedObj)
    {
        _weaponScript.PlayerReleaseAction();
        base.ThrowItem (trackedObj);

    }


}
