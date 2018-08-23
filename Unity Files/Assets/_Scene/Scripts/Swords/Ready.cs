using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is the state of the enemy after attacking, the enemy will wait for a few seconds before getting their next action
/// Enemy will behave similar to their idle state, except they will constantly maintain around 2 meters from the player
/// </summary>
public class Ready : AIState
{
    [SerializeField] private int _strafeDir;

    private float _forwardSpeed = 3.0f;

    private float _strafeSpeed = 1.0f;

    Vector3 _targetPosition;

    // Here, _timer will be used to count down how long before the enemy can take their next action


    /// <summary>
    /// back off, strafe around the player slowly, maintaining a certain distance, then end the state
    /// </summary>



    // Use this for initialization
    void Start()
    {
        _minDist = 2.0f;
        _maxDist = 2.5f;

        _timer = Random.Range(1.0f, 5.0f);


            // Initialize strafe direction
            if (Random.Range(1, 3) == 1)
            {
                _strafeDir = 1;
            }
            else
            {
                _strafeDir = -1;
            }
    }
	
    // Update is called once per frame
    void Update()
    {
        _targetPosition = _target.transform.position;
        _targetPosition.y = gameObject.transform.position.y;

//        Vector3 posToLook = new Vector3(_target.transform.position.x,
//            transform.position.y,
//            _target.transform.position.z);

        gameObject.transform.LookAt(_targetPosition);

        if (_animator.GetBool("backingOff"))
        {
            // if current animation is backing off

            _rb.MovePosition(gameObject.transform.position - (gameObject.transform.forward * _forwardSpeed * Time.deltaTime));
            //gameObject.transform.position -= gameObject.transform.forward * 3.0f * Time.deltaTime;

            // Increase max distance to keep self on safer distance
            //_maxDist = (gameObject.transform.position - _target.transform.position).magnitude;
            _maxDist = (gameObject.transform.position - _targetPosition).magnitude;
        }
        else if(!_animator.GetBool("isTakingDamage") && !_animator.GetBool("offBalance"))
        {
            // if not in taking damage animation, since the state will instantly change to this when damage is taken
            if (InRange())
            {
                Waiting();
            }

            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _stateComplete = true;
            }
        }
    }

    public override void EndStateAction()
    {
        _stateComplete = false;
        _timer = Random.Range(1.0f, 5.0f);

        _minDist = 2.0f;
        _maxDist = 2.5f;
    }


    /// <summary>
    /// Determines if the gameObject is currently within range of the target
    /// </summary>
    /// <returns><c>true</c>, if within range <c>false</c> if not </returns>
    private bool InRange()
    {
        bool withinRange = true;

        //float distanceFromTarget = (gameObject.transform.position - _target.transform.position).magnitude;
        float distanceFromTarget = (gameObject.transform.position - _targetPosition).magnitude;

        if (distanceFromTarget > _maxDist)
        {
            _animator.SetBool("forward", true);

            _animator.SetFloat("forwardSpeed", 1.0f);

            withinRange = false;

            _rb.MovePosition(gameObject.transform.position + (gameObject.transform.forward * _forwardSpeed * Time.deltaTime));
            //gameObject.transform.position += gameObject.transform.forward * 3.0f * Time.deltaTime;
        }
        else if (distanceFromTarget < _minDist)
        {
            _animator.SetBool("forward", true);

            _animator.SetFloat("forwardSpeed", -1.0f);

            withinRange = false;

            _rb.MovePosition(gameObject.transform.position - (gameObject.transform.forward * _forwardSpeed * Time.deltaTime));
            //gameObject.transform.position -= gameObject.transform.forward * 3.0f * Time.deltaTime;
        }

        return withinRange;
    }


    /// <summary>
    /// strafe around the player
    /// </summary>
    private void Waiting()
    {
        _animator.SetBool("forward", false);

        _animator.SetFloat("rightSpeed", 1.0f);

        _rb.MovePosition(gameObject.transform.position - (gameObject.transform.right * _strafeSpeed * _strafeDir * Time.deltaTime));
        //gameObject.transform.position += gameObject.transform.right * _strafeDir * 1.0f * Time.deltaTime;


    }
}
