using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordExit : InteractableItem  {

    public override void OnObjectInteractHold (GameObject hand, Animator anim, Transform grabPoint)
    {
        Debug.Log("exit");
		FindObjectOfType<AdditiveSceneMethod.AdditiveSceneController> ().CallToTavern ();
		FindObjectOfType<TeleportVive> ().enabled = true;
		FindObjectOfType<TeleportVive> ().AssignPointerMesh ();

    }


    public override void OnObjectInteractRelease (GameObject hand, Animator anim)
    {
    }
}
