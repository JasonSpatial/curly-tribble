using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{

    public GameObject triggerObject;

    // public GameObject collisionObject;
    // need to define some collision events that PlayerManager can
    // subscribe to.
    // these would be for triggers as well as collisions

    // public event Action<PlayerCollisionManager> OnCollision;  
    public event Action<PlayerCollisionManager> OnTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"triggering {other}");
        triggerObject = other.gameObject;
        OnTrigger?.Invoke(this);
        Destroy(other.gameObject);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log($"colliding with {other}");
    //     collisionObject = other.gameObject;
    //     OnCollision?.Invoke(this);
    //     
    // }

}
