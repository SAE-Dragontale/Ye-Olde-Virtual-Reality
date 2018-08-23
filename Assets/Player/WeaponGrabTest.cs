using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrabTest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hoverList;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _gripTransform;

    // Use this for initialization
    void Start()
    {
        _hoverList = new List<GameObject>();
        _animator = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Grip();
        }
            
        transform.position += new Vector3(0, 0, Input.GetAxis("Vertical") * Time.deltaTime);
    }

    public void Grip()
    {
        if (_hoverList.Count > 0)
        {
            _animator.SetInteger("_grabIndex", 2);
            _hoverList[0].GetComponent<TestWeaponScript>().Interact(_gripTransform);
        }
        else
        {
            Debug.Log("test");
        }
    }

    void OnTriggerEnter(Collider inCollide)
    {
        _hoverList.Add(inCollide.gameObject);
        _animator.SetInteger("_hoverCount", _hoverList.Count);
    }


    void OnTriggerExit(Collider inCollide)
    {
        _hoverList.Remove(inCollide.gameObject);
        _animator.SetInteger("_hoverCount", _hoverList.Count);
    }
}
