using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // private PlayerCollisionManager _collisionManager;
    private Rigidbody2D _rb;
    [SerializeField] private float _forceFactor = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        // _collisionManager = FindObjectOfType<PlayerCollisionManager>();
        // _collisionManager.OnCollision += HandleCollision;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         var collisionVector = (other.gameObject.transform.position - transform.position).normalized;
    //         Debug.Log($"collision {collisionVector}");
    //         // _rb.AddRelativeForce(-collisionVector * _forceFactor, ForceMode2D.Impulse);
    //         _rb.AddForceAtPosition(-collisionVector * _forceFactor, transform.position, ForceMode2D.Impulse);
    //     }
    // }

    // void HandleCollision(PlayerCollisionManager collisionManager)
    // {
    //     Debug.Log($"colliding object {collisionManager.transform.position}");
    //     Debug.Log($"Obstacle position: {transform.position}");
    //     var collisionVector = (collisionManager.transform.position - transform.position).normalized;
    //     Debug.Log($"collision {collisionVector}");
    //     _rb.AddForceAtPosition(-collisionVector * _forceFactor, transform.position, ForceMode2D.Impulse);
    // }
}
