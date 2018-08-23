using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This AIState will not reach a state of _stateComplete == true
// Fight coordinator will make the EnemyAIManager disable this state and enable attack

public class Idle : AIState
{
    [SerializeField] private int _strafeDir;
    [SerializeField] private float _speed = 2.0f;

    [SerializeField] private float _range;


    // Here, _timer is used to calculate when to change strafe direction

    // Use this for initialization
    void Start()
    {
        // Initialize distance
        _minDist = Random.Range(5.0f, 10.0f);
        _maxDist = Random.Range(10.0f, 15.0f);



        // Initialize strafe direction
        if(Random.Range(1, 3) == 1)
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

        Vector3 posToLook = new Vector3(_target.transform.position.x,
                                        transform.position.y,
                                        _target.transform.position.z);

        gameObject.transform.LookAt(posToLook);

        if (InRange())
        {
            _animator.SetBool("forward", false);


            Waiting();
        }
    }

    /// <summary>
    /// Determines if the gameObject is currently within range of the target
    /// </summary>
    /// <returns><c>true</c>, if within range <c>false</c> if not </returns>
    private bool InRange()
    {
        bool withinRange = true;

        Vector3 targetPosition = _target.transform.position;
        targetPosition.y = gameObject.transform.position.y;

        //float distanceFromTarget = (gameObject.transform.position - _target.transform.position).magnitude;
        float distanceFromTarget = (gameObject.transform.position - targetPosition).magnitude;

        if (distanceFromTarget > _maxDist)
        {
            _animator.SetBool("forward", true);

            _animator.SetFloat("forwardSpeed", _speed);

            withinRange = false;

            _rb.MovePosition(gameObject.transform.position + (gameObject.transform.forward * _speed * Time.deltaTime));
            //gameObject.transform.position += gameObject.transform.forward * _speed * Time.deltaTime;
        }
        else if (distanceFromTarget < _minDist)
        {
            _animator.SetBool("forward", true);

            _animator.SetFloat("forwardSpeed", -1 * _speed);


            withinRange = false;


            _rb.MovePosition(gameObject.transform.position - (gameObject.transform.forward * _speed * Time.deltaTime));
            //gameObject.transform.position -= gameObject.transform.forward * _speed * Time.deltaTime;
        }

        return withinRange;
    }


    /// <summary>
    /// Wait for a signal to attack, in the meantime, strafe around the player
    /// </summary>
    private void Waiting()
    {
        if (_timer <= 0)
        {
            _timer = Random.Range(0.5f, 5.0f);
            _strafeDir *= -1;

            _animator.SetFloat("rightSpeed", _speed * _strafeDir);
        }


        _rb.MovePosition(gameObject.transform.position + (gameObject.transform.right * _strafeDir * _speed * Time.deltaTime));
        //gameObject.transform.position += gameObject.transform.right * _strafeDir * _speed * Time.deltaTime;


        _timer -= Time.deltaTime;
    }

    public override void EndStateAction()
    {
    }
}
