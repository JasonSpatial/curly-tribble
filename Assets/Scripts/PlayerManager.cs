using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum BoostState { NotBoosting, StartedBoosting, IsBoosting, FinishedBoosting }

public class PlayerManager : MonoBehaviour
{
    private Scroller _scroller;
    private PlayerCollisionManager _collisionManager;

    private PlayerMover _playerMover;
    [SerializeField]
    private BoostState _boostState = BoostState.NotBoosting;
    
    [SerializeField] private float scrollSpeed, boostRemaining, boostLength, moveSpeed, boostSpeed;

    [SerializeField] private bool isShielding, isSlowed;
    
    
    // create events in collision manager that we can subscribe to
    // to know what we've hit so we can decide here what to do about it
    private void OnEnable()
    {
        _scroller = FindObjectOfType<Scroller>();
        _collisionManager = GetComponent<PlayerCollisionManager>();
        _playerMover = GetComponent<PlayerMover>();
        
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
    // private PlayerInput _input;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Boost()
    {
        scrollSpeed = _scroller._scrollSpeed;
        _boostState = BoostState.IsBoosting;
        boostRemaining = boostLength;
        _scroller._scrollSpeed += boostSpeed;
    }

    void FinishBoosting()
    {
        _boostState = BoostState.NotBoosting;
        _scroller._scrollSpeed = scrollSpeed;
    }
    
    // Update is called once per frame
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
            // add to moveSpeed
        }
        else
        {
            // show normal engine animation
            // reset moveSpeed
        }
    }
}
