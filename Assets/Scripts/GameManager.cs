using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // TODO
    // - Fix keeping track of distance travelled
    // - Move pickup logic to PlayerManager and Pickup script (need to create) and get rid of events
    
    public event Action<GameManager> OnGamePause;
    public event Action<GameManager> OnGameUnpaused;
    public event Action<GameManager> OnRoundOver; 
    
    public static GameManager Instance { get; private set; }

    private PlayerManager _playerManager;
    private Scroller _scroller;
        
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
            OnRoundOver?.Invoke(this);
        }
        else
        {
            distanceTravelled += _scroller.scrollSpeed;
        }

    }

    private void Awake()
    {
        _scroller = FindObjectOfType<Scroller>();
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
