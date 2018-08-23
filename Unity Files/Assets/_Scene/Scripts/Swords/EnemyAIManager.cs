using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class EnemyAIManager : MonoBehaviour
{
    private FightCoordinator    _coordinator;

    [SerializeField] private int _life = 3;

    [SerializeField] private List<AIState>  _stateScript;
    private int _stateIndex = 0;

    [SerializeField] private GameObject          _target;

    [SerializeField] private Animator  _animator;

    [SerializeField] private float _deathTimer = 5.0f;

    // The empty game object which serves as the grip point
    [SerializeField] private Transform _gripTransform;
    [SerializeField] private List<GameObject> _weaponsList;

    [SerializeField] private AudioSource _voice;

    [SerializeField] private List<AudioClip> _warCries;
    [SerializeField] private List<AudioClip> _grunts;
    [SerializeField] private List<AudioClip> _deathScreams;

    private GameObject _weapon;

    // flag to double check and ensure all state script is disabled
    private bool _doubleCheck = false;

    private float _invincibilityFrame = 0.0f;

    [SerializeField] private Rigidbody _rb;

    // Use this for initialization
    void Start()
    {
        _voice = GetComponent<AudioSource>();

        _rb = GetComponent<Rigidbody>();

        _weapon = Instantiate(_weaponsList[Random.Range(0, _weaponsList.Count)],_gripTransform);
        _weapon.tag = "EnemyWeapon";
        _weapon.GetComponent<WeaponScript>().InitializeEnemy(this);


        Rigidbody weaponRB = _weapon.GetComponent<Rigidbody>();

        _stateScript = new List<AIState>();

        _stateScript.Add(gameObject.AddComponent<Idle>());
        _stateScript.Add(gameObject.AddComponent<Attack>());
        _stateScript.Add(gameObject.AddComponent<Ready>());

        foreach (AIState state in _stateScript)
        {
            state.InitializeState(_target, _animator, _rb);
            state.enabled = false;
        }

        _stateScript[_stateIndex].enabled = true;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_animator.GetBool("death"))
        {
            if (!_doubleCheck)
            {
                foreach (AIState state in _stateScript)
                {
                    state.enabled = false;
                }
            }

            _deathTimer -= Time.deltaTime;

            if (_deathTimer <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (_stateScript[_stateIndex].StateComplete())
            {
                ChangeState();
            }
        }

        if (_invincibilityFrame > 0.0f)
        {
            _invincibilityFrame -= Time.deltaTime;
        }
    }


    /// <summary>
    /// Determine the next state the AI should be at
    /// </summary>
    private void ChangeState()
    {
        _stateScript[_stateIndex].EndStateAction();
        _stateScript[_stateIndex].enabled = false;
        _stateIndex++;

        // if the state index reached 2, that means that the AI had completed the attack and recover cycle
        if (_stateIndex > 2)
        {
            _stateIndex = _coordinator.GetNextInstruction(gameObject);

            if (_stateIndex == 1)
            {
                WarCry();
            }
            //_stateScript[_stateIndex].enabled = true;
        }

        _stateScript[_stateIndex].enabled = true;
    }


    /// <summary>
    /// Sets the target that this enemy will lock on to
    /// </summary>
    /// <param name="inTarget">In target.</param>
    public void CoordinatorInitialize(GameObject inTarget, FightCoordinator inCoordinator)
    {
        _coordinator = inCoordinator;
        _target = inTarget;
    }


    /// <summary>
    /// Signals the attack.
    /// </summary>
    /// <returns><c>true</c>, if attack instruction accepted <c>false</c> attack instruction rejected</returns>
    public bool SignalAttack()
    {
        bool acceptSignal = false;

        if (_stateIndex == 0)
        {
            ChangeState();
            acceptSignal = true;

            Debug.Log(gameObject.name + " accept attack signal");

            WarCry();
        }
        else
        {
            Debug.Log(gameObject.name + " reject attack signal");
        }

        return acceptSignal;
    }

    public void Parried()
    {
        _animator.SetTrigger("parried");

        _stateScript[_stateIndex].EndStateAction();
        _stateScript[_stateIndex].enabled = false;
        _stateIndex = 2;

        _stateScript[_stateIndex].enabled = true;
    }

    private void DeathAction()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "PlayerWeapon" || collider.transform.parent.gameObject.tag == "PlayerWeapon")
        {
            if (_invincibilityFrame <= 0.0f)
            {
                _life--;
                _invincibilityFrame = 1.0f;
            



                if (_life <= 0 && !_animator.GetBool("death"))
                {
                    Debug.Log("died");
                    _animator.SetBool("death", true);
                    _stateScript[_stateIndex].enabled = false;

                    PlayVoice(_deathScreams[Random.Range(0, _deathScreams.Count)]);



                    _weapon.GetComponent<WeaponScript>().EnemyReleaseWeaponAction();
                    //Debug.Log(_weapon.tag);

                    GetComponent<Rigidbody>().detectCollisions = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    _coordinator.EnemyKilled(gameObject);




                }
                else if (_life > 0)
                {
                    PlayVoice(_grunts[Random.Range(0, _grunts.Count)]);
                    _animator.SetTrigger("damaged");

                    _stateScript[_stateIndex].EndStateAction();
                    _stateScript[_stateIndex].enabled = false;
                    _stateIndex = 2;

                    _stateScript[_stateIndex].enabled = true;
                }
            }
        }
    }

    private void PlayVoice(AudioClip inClip)
    {
        _voice.clip = inClip;
        _voice.Play();
    }

    public void WarCry()
    {
        PlayVoice(_warCries[Random.Range(0, _warCries.Count)]);
    }

    public void GameOver()
    {
        GetComponent<Rigidbody>().detectCollisions = false;
        GetComponent<Rigidbody>().isKinematic = true;

        _stateScript[_stateIndex].EndStateAction();
        _stateScript[_stateIndex].enabled = false;

        _animator.SetBool("GameOver", true);
    }
}
