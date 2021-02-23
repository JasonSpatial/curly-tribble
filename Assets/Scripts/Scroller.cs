using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    
    public float _scrollSpeed;
    [SerializeField]
    private GameObject background1, background2;

    [SerializeField]
    private float _textureWidth;

    private float _originalScrollSpeed;

    private PlayerManager _playerManager;
    

    // Update is called once per frame
    void Update()
    {
        var bg1Pos = background1.transform.position;
        var bg2Pos = background2.transform.position;

        
        background1.transform.position =
            new Vector3(bg1Pos.x - _scrollSpeed * Time.deltaTime, bg1Pos.y, bg1Pos.z);

        background2.transform.position = new Vector3(bg2Pos.x - _scrollSpeed * Time.deltaTime, bg1Pos.y, bg1Pos.z);
        
        if (bg1Pos.x < _textureWidth)
        {
            background1.transform.position = new Vector3(bg2Pos.x + Mathf.Abs(_textureWidth), bg1Pos.y, bg1Pos.z);
        }
        if (bg2Pos.x < _textureWidth)
        {
            background2.transform.position = new Vector3(bg1Pos.x + Mathf.Abs(_textureWidth), bg2Pos.y, bg2Pos.z);
        }
        
    }
    
    private void OnEnable()
    {
        _playerManager = FindObjectOfType<PlayerManager>();

        _playerManager.OnStartBoost += ActivateBoost;
        _playerManager.OnEndBoost += DeactivateBoost;
        
        GameManager.Instance.OnRoundOver += HandleGameOver;
        GameManager.Instance.OnGamePause += HandleGamePaused;
        GameManager.Instance.OnGameUnpaused += HandleGameUnpause;
    }

    void ActivateBoost(PlayerManager playerManager)
    {
        _originalScrollSpeed = _scrollSpeed;
        _scrollSpeed += playerManager.boostSpeed;
    }

    void DeactivateBoost(PlayerManager playerManager)
    {
        _scrollSpeed = _originalScrollSpeed;
    }
    
    private void HandleGameOver(GameManager gameManager)
    {
        _scrollSpeed = 0;
    }

    private void HandleGamePaused(GameManager gameManager)
    {
        _originalScrollSpeed = _scrollSpeed;
        _scrollSpeed = 0;
    }

    private void HandleGameUnpause(GameManager gameManager)
    {
        _scrollSpeed = _originalScrollSpeed;
    }

}
