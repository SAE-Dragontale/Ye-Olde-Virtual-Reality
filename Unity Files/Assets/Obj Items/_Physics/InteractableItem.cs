using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public abstract class InteractableItem : MonoBehaviour {

	public GameObject gripPoint;
	[HideInInspector]
	public bool _isInteractedWith;


	public abstract void OnObjectInteractHold (GameObject hand, Animator anim, Transform grabPoint);


	public abstract void OnObjectInteractRelease (GameObject hand, Animator anim);



}
