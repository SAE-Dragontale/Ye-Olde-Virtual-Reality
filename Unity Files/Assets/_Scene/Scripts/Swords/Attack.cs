using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AIState
{


    [SerializeField] private int _phase = 0;

    private bool _shoutTrigger = true;
    private bool _rangeTrigger = true;

    private float _speed = 4;

    // Update is called once per frame
    void Update()
    {
		/*
         * Attacking state has 3 action
         * 1. Shout
         * 2. Approach
         * 3. Strike
         */

        switch (_phase)
        {
            case 0:
                if (_shoutTrigger)
                {
                    Shout();
                }
                break;
            case 1:
                Approach();
                break;
            case 2:
                Strike();
                break;
            case 3:
                _stateComplete = true;
                break;
        }

    }

    void LateUpdate()
    {

        switch (_phase)
        {
            case 0:
                if (!_animator.GetBool("isShouting") && !_shoutTrigger)
                {
                    _phase++;
                }
                break;
            case 1:
                //Approach();
                break;
            case 2:
                //Strike();
                break;
            case 3:
                //_stateComplete = true;
                break;
        }
    }


    private void Shout()
    {
        _animator.SetTrigger("attack");

        _shoutTrigger = false;
    }


    private void Approach()
    {
        Vector3 targetPosition = _target.transform.position;
        targetPosition.y = gameObject.transform.position.y;

        if (Mathf.Abs((gameObject.transform.position - targetPosition).magnitude) >= 1.5f)
        {
//            Vector3 posToLook = new Vector3(_target.transform.position.x,
//                transform.position.y,
//                _target.transform.position.z);

            gameObject.transform.LookAt(targetPosition);

            _rb.MovePosition(gameObject.transform.position + (gameObject.transform.forward * _speed * Time.deltaTime));
            //gameObject.transform.position += gameObject.transform.forward * _speed * Time.deltaTime;
        }
        else
        {
            // when in range, set animator trigger inRange to begin attacking animation
            if (_rangeTrigger && !_shoutTrigger)
            {
                _animator.SetTrigger("inRange");
                _rangeTrigger = false;
            }

            if (_animator.GetBool("isAttacking"))
            {
                // double check that the animation isAttacking boolean is true, meaning the animation had begin before changing phase
                _phase++;
            }
        }
    }

    private void Strike()
    {
        if (!_animator.GetBool("isAttacking"))
        {
            // when isAttacking is false, the animation had ended, change phase
            _phase++;
        }
    }

    public override void EndStateAction()
    {
        _shoutTrigger = true;
        _rangeTrigger = true;
        _phase = 0;
        _stateComplete = false;
    }
}
