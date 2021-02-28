using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{

    [SerializeField] private List<Sprite> _destinations;

    [SerializeField]
    private float _destinationDistanceBuffer;
    
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
        return _destinations[Random.Range(0, _destinations.Count)];
    }
    


    void Update()
    {
        if (GameManager.Instance._gameState == GameStates.Started && GameManager.Instance.distanceRemaining <= _destinationDistanceBuffer)
        {
            Vector2 pos = transform.position;
            
            transform.position =
                new Vector3(pos.x - (_scroller.scrollSpeed/4) * Time.deltaTime, pos.y, 0);
        }
    }
}
