using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class PickupMover : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    private float _originalMoveSpeed;

    private PlayerManager _playerManager;
    private PlayerMover _playerMover;


    
    void Awake()
    {
        moveSpeed = Random.Range(-2f, -0.5f);
        _originalMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerMover = FindObjectOfType<PlayerMover>();

        _playerManager.OnStartBoost += ActivateBoost;
        _playerManager.OnEndBoost += DeactivateBoost;
        _playerMover.OnRoundOver += RoundOver;

        // moveSpeed = -_playerMover.moveSpeed;
    }

    void ActivateBoost(PlayerManager playerManager)
    {
        Debug.Log($"Setting _originalMoveSpeed to {_originalMoveSpeed}");
        _originalMoveSpeed = moveSpeed;
        moveSpeed += -playerManager.boostSpeed;
    }

    public void RoundOver(PlayerMover playerMover)
    {
        Debug.Log("Round over?");
        moveSpeed = playerMover.moveSpeed;
    }
    
    void DeactivateBoost(PlayerManager playerManager)
    {
        Debug.Log($"Resetting speed to {_originalMoveSpeed}");
        moveSpeed = _originalMoveSpeed;
    }
    void Update()
    {
        var pos = transform.position;
            var xPos = pos.x + (moveSpeed * Time.deltaTime);

            var yPos= pos.y;
            transform.position = new Vector3(xPos, yPos, 0);

    }
    
}
