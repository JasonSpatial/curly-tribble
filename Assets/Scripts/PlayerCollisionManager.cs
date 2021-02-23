using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{

    public GameObject triggerObject;
    // need to define some collision events that PlayerManager can
    // subscribe to.
    // these would be for triggers as well as collisions

    public event Action<PlayerCollisionManager> OnCollision;  
    public event Action<PlayerCollisionManager> OnTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        triggerObject = other.gameObject;
            OnTrigger?.Invoke(this);
            Destroy(other.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
