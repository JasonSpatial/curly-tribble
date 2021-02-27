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
        
    [SerializeField] private float forceFactor, boostRemaining, boostLength, moveSpeed;

    [SerializeField] protected bool isShielding, isSlowed;

    private Rigidbody2D _rb;

    Camera cam => Camera.main;
    
    
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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void HandleTrigger(PlayerCollisionManager collisionManager)
    {
        if (collisionManager.triggerObject.CompareTag("Fuel"))
        {
            Boost();
        }
    }

    void Boost()
    {
        if (_boostState == BoostState.IsBoosting)
        {
            boostRemaining += boostLength;
        }
        else
        {
            _boostState = BoostState.IsBoosting;
            boostRemaining = boostLength;
            _animator.SetBool("IsBoosting", true);
            OnStartBoost?.Invoke(this);
           
        }
    }

    
    
    void FinishBoosting()
    {
        _boostState = BoostState.NotBoosting;
        _animator.SetBool("IsBoosting", false);
        OnEndBoost?.Invoke(this);
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Obstacle"))
        {
            var otherMover = other.gameObject.GetComponent<PickupMover>();
            var otherRB = other.gameObject.GetComponent<Rigidbody2D>();
            var otherCollider = other.gameObject.GetComponent<PolygonCollider2D>();

            AkSoundEngine.PostEvent("Play_Impact", cam.gameObject);

            otherMover.enabled = false;
            
            var collisionVector = (transform.position - other.gameObject.transform.position).normalized;
            // otherCollider.enabled = false;
            otherRB.AddForceAtPosition(-collisionVector * forceFactor, transform.position, ForceMode2D.Impulse);
        }
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

        }

    }
}
