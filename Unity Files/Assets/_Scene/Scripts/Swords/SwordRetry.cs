using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRetry : InteractableItem  {

    public override void OnObjectInteractHold (GameObject hand, Animator anim, Transform grabPoint)
    {
        Debug.Log("retry");
		FindObjectOfType<AdditiveSceneMethod.AdditiveSceneController> ().CallReload (3);
    }


    public override void OnObjectInteractRelease (GameObject hand, Animator anim)
    {
    }
}
