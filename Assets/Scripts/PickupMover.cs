using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class PickupMover : MonoBehaviour
{
    public float moveSpeed;
    private float _originalMoveSpeed;

    private PlayerManager _playerManager;
    
    void Start()
    {
        moveSpeed = Random.Range(-2f, -0.5f);
    }

    private void OnEnable()
    {
        _playerManager = FindObjectOfType<PlayerManager>();

        _playerManager.OnStartBoost += ActivateBoost;
        _playerManager.OnEndBoost += DeactivateBoost;
    }

    void ActivateBoost(PlayerManager playerManager)
    {
        _originalMoveSpeed = moveSpeed;
        moveSpeed += -playerManager.boostSpeed;
    }

    void DeactivateBoost(PlayerManager playerManager)
    {
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
