using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private EnemyAIManager _manager = null;

    private Rigidbody weaponRB;

    [SerializeField] private AudioClip _clashSFX;
    [SerializeField] private AudioClip _fall;
    [SerializeField] private AudioClip _damage;

    private AudioSource _audio;

    // used to turn off prevent parry from being called multiple time
    private float _parryTimer = 0.0f;

    // Use this for initialization
    void Start()
    {
        weaponRB = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_parryTimer > 0.0f)
        {
            _parryTimer -= Time.deltaTime;
        }
        else if (_parryTimer <= 0.0f && GetComponent<Rigidbody>().detectCollisions == false)
        {
            GetComponent<Rigidbody>().detectCollisions = true;
        }
    }



    public void InitializeEnemy(EnemyAIManager inManager)
    {
        GetComponentInChildren<Collider>().isTrigger = true;
        _manager = inManager;
    }

    public void PlayerPickupAction()
    {
		Debug.Log ("test");
        GetComponentInChildren<Collider>().isTrigger = true;
        gameObject.tag = "PlayerWeapon";
        //gameObject.
    }

    public void PlayerReleaseAction()
    {
        GetComponentInChildren<Collider>().isTrigger = false;
    }

    public void EnemyReleaseWeaponAction()
    {
        gameObject.tag = "Untagged";
        gameObject.transform.parent = null;
        gameObject.layer = 0;

        weaponRB.useGravity = true;
        weaponRB.isKinematic = false;
        GetComponentInChildren<Collider>().isTrigger = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.parent != null)
        {
            ProcessOnTriggerEnterWithParent(collider);
        }
        else
        {
            ProcessOnTriggerEnter(collider);
        }
    }

    void ProcessOnTriggerEnterWithParent(Collider collider)
    {
        if (((collider.gameObject.tag == "PlayerWeapon" || collider.gameObject.transform.parent.tag == "PlayerWeapon") && (gameObject.tag == "EnemyWeapon")) ||
            ((collider.gameObject.tag == "EnemyWeapon" || collider.gameObject.transform.parent.tag == "EnemyWeapon") && (gameObject.tag == "PlayerWeapon")))
        {
            if (_manager != null && _parryTimer <= 0.0f)
            {
                // when this sword is held by the enemy and clashed with PlayerWeapon
                _manager.Parried();
                _parryTimer = 1.0f;
                GetComponent<Rigidbody>().detectCollisions = false;
            }
            PlaySFX(_clashSFX);
        }
        else if ((collider.gameObject.tag == "Player" && gameObject.tag == "EnemyWeapon") || (collider.gameObject.tag == "Enemy" && gameObject.tag == "PlayerWeapon"))
        {
            Debug.Log("damage");
            PlaySFX(_damage);
        }
    }

    void ProcessOnTriggerEnter(Collider collider)
    {
        if (((collider.gameObject.tag == "PlayerWeapon") && (gameObject.tag == "EnemyWeapon") ||
			((collider.gameObject.tag == "EnemyWeapon") && (gameObject.tag == "PlayerWeapon"))))
        {
            if (_manager != null && _parryTimer <= 0.0f)
            {
                // when this sword is held by the enemy and clashed with PlayerWeapon
                _manager.Parried();
                _parryTimer = 1.0f;
                GetComponent<Rigidbody>().detectCollisions = false;
            }
            PlaySFX(_clashSFX);
        }
        else if ((collider.gameObject.tag == "Player" && gameObject.tag == "EnemyWeapon") || (collider.gameObject.tag == "Enemy" && gameObject.tag == "PlayerWeapon"))
        {
            Debug.Log("damage");
            PlaySFX(_damage);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            PlaySFX(_fall);
            gameObject.tag = "Untagged";
        }
    }

    private void PlaySFX(AudioClip inClip)
    {
        _audio.clip = inClip;
        _audio.Play();
    }
}
