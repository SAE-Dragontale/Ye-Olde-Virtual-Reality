using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericUIWindowScript : MonoBehaviour
{
    Animator anim;
	// Use this for initialization
	public virtual void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	public void Show()
    {
        anim.SetTrigger("Open");
    }

    public void Hide()
    {
        anim.SetTrigger("Close");
    }
}
