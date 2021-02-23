using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public event Action<GameManager> OnGamePause;
    public event Action<GameManager> OnGameUnpaused;
    public event Action<GameManager> OnRoundOver; 
    
    public static GameManager Instance { get; private set; }

    private PlayerManager _playerManager;
        
    [SerializeField] private int levelDistance;
    [SerializeField] private float distanceTravelled;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    void Pause()
    {
        OnGamePause?.Invoke(this);
    }

    void Unpause()
    {
        OnGameUnpaused?.Invoke(this);
    }

    void Quit()
    {
        Application.Quit();    
    }
    
    void Update()
    {
        if (distanceTravelled >= levelDistance)
        {
            Debug.Log("Round over");
            OnRoundOver?.Invoke(this);
        }
        else
        {
            distanceTravelled += _playerManager.scrollSpeed;
        }

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
    
}
