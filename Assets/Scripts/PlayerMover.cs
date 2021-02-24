using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    public event Action<PlayerMover> OnRoundOver; 
    
    [SerializeField] private float backClamp, frontClamp, topClamp, bottomClamp;
    public float moveSpeed;
    private Vector3 _moveVec;

    private float _originalMoveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var shipPos = transform.position;
        if (_moveVec != Vector3.zero)
        {
            var rawXPos = shipPos.x + (moveSpeed * _moveVec.x * Time.deltaTime);
            var xPos = Mathf.Clamp(rawXPos, backClamp, frontClamp);

            var rawYPos= shipPos.y + (moveSpeed * _moveVec.y * Time.deltaTime);
            var yPos = Mathf.Clamp(rawYPos, bottomClamp, topClamp);

            transform.position = new Vector3(xPos, yPos, 0);
        }

    }

    void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        _moveVec = new Vector3(inputVec.x, inputVec.y, 0);
    }
    
    private void OnEnable()
    {
        // GameManager.Instance.OnRoundOver += HandleRoundOver;
        // GameManager.Instance.OnGamePause += HandleGamePaused;
        // GameManager.Instance.OnGameUnpaused += HandleGameUnpause;
    }

    public void RoundOver()
    {
        Debug.Log("round over playermover");
        moveSpeed = 0;
        OnRoundOver?.Invoke(this);
    }

    public void GamePaused()
    {
        _originalMoveSpeed = moveSpeed;
        moveSpeed = 0;
    }

    public void GameUnpause()
    {
        moveSpeed = _originalMoveSpeed;
    }

}
