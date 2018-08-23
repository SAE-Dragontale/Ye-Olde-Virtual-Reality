using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    [SerializeField] protected GameObject _target = null;
    [SerializeField] protected float _timer = 0.0f;

    [SerializeField] protected bool _stateComplete = false;

    [Tooltip("The minimum distance which the enemy need to distance himself")]
    [SerializeField] protected float _minDist = 2.0f;

    [Tooltip("The maximum distance which the enemy need to distance himself")]
    [SerializeField] protected float _maxDist = 10.0f;

    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody _rb;


    // <summary>
    /// Sets the target that this enemy will lock on to
    /// </summary>
    /// <param name="inTarget">In target.</param>
    public void InitializeState(GameObject inTarget, Animator inAnimator, Rigidbody inRB)
    {
        _target = inTarget;
        _animator = inAnimator;
        _rb = inRB;
    }


    /// <summary>
    /// Return a boolean to indicate whether this state reached it's end
    /// </summary>
    /// <returns><c>true</c>, if state complete <c>false</c> otherwise.</returns>
    public bool StateComplete()
    {
        return _stateComplete;
    }

    public abstract void EndStateAction();

}
