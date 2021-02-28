using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using Random = UnityEngine.Random;

public class Background : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgrounds;
    private SpriteRenderer _renderer;
    private Scroller _scroller;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _renderer.sprite = PickASprite();
        _scroller = FindObjectOfType<Scroller>();

    }

    private Sprite PickASprite()
    {
        return backgrounds[Random.Range(0, backgrounds.Count)];
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance._gameState == GameStates.Started)
        {
            Vector2 pos = transform.position;
            
            transform.position =
                new Vector3(pos.x - (_scroller.scrollSpeed/4) * Time.deltaTime, pos.y, 0);
        }
    }
}
