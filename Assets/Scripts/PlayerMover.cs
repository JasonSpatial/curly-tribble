using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMover : MonoBehaviour
{
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
        GameManager.Instance.OnRoundOver += HandleRoundOver;
        GameManager.Instance.OnGamePause += HandleGamePaused;
        GameManager.Instance.OnGameUnpaused += HandleGameUnpause;
    }

    private void HandleRoundOver(GameManager gameManager)
    {
        moveSpeed = 0;
    }

    private void HandleGamePaused(GameManager gameManager)
    {
        _originalMoveSpeed = moveSpeed;
        moveSpeed = 0;
    }

    private void HandleGameUnpause(GameManager gameManager)
    {
        moveSpeed = _originalMoveSpeed;
    }

}
