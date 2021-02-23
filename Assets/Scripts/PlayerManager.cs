using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum BoostState { NotBoosting, StartedBoosting, IsBoosting, FinishedBoosting }

public class PlayerManager : MonoBehaviour
{
    private Scroller _scroller;
    private PickupMover _pickupMover;
    [SerializeField]
    private Animator _animator;
    public event Action<PlayerManager> OnStartBoost;
    public event Action<PlayerManager> OnEndBoost;

    private PlayerCollisionManager _collisionManager;

    private PlayerMover _playerMover;
    [SerializeField]
    private BoostState _boostState = BoostState.NotBoosting;

    public float scrollSpeed, boostSpeed;
        
    [SerializeField] private float boostRemaining, boostLength, moveSpeed;

    [SerializeField] protected bool isShielding, isSlowed;
    
    
    
    // create events in collision manager that we can subscribe to
    // to know what we've hit so we can decide here what to do about it
    private void OnEnable()
    {
        _scroller = FindObjectOfType<Scroller>();
        _collisionManager = GetComponent<PlayerCollisionManager>();
        _playerMover = GetComponent<PlayerMover>();
        _animator = GetComponentInChildren<Animator>();
        
        _collisionManager.OnTrigger += HandleTrigger;
    }

    
    void HandleTrigger(PlayerCollisionManager collisionManager)
    {
        Debug.Log("handling a trigger");
        Debug.Log(collisionManager.triggerObject.tag);
        if (collisionManager.triggerObject.CompareTag("Fuel"))
        {
            Debug.Log("boosting");
            Boost();
        }
    }

    void Boost()
    {
        _boostState = BoostState.IsBoosting;
        boostRemaining = boostLength;
        _animator.SetBool("IsBoosting", true);
        OnStartBoost?.Invoke(this);
    }

    void FinishBoosting()
    {
        _boostState = BoostState.NotBoosting;
        _animator.SetBool("IsBoosting", false);
        OnEndBoost?.Invoke(this);
    }
    
    void Update()
    {
        if (_boostState == BoostState.IsBoosting)
        {
            boostRemaining -= Time.deltaTime;
            if (boostRemaining <= 0f)
            {
                FinishBoosting();
            }
            // show boosting animation
        }
        else
        {
            // show normal engine animation
        }
    }
}
