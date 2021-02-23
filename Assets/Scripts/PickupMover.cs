using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PickupMover : MonoBehaviour
{
    public float moveSpeed;

    void Start()
    {
        
    }
    void Update()
    {
        var pos = transform.position;
            var xPos = pos.x + (moveSpeed * Time.deltaTime);

            var yPos= pos.y;
            transform.position = new Vector3(xPos, yPos, 0);

    }
    
}
