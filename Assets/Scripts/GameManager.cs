using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // TODO
    // - Fix keeping track of distance travelled
    // - Move pickup logic to PlayerManager and Pickup script (need to create) and get rid of events
    
    // public event Action<GameManager> OnGamePause;
    // public event Action<GameManager> OnGameUnpaused;
    // public event Action<GameManager> OnRoundOver; 
    
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PlayerManager _playerManager;
    [SerializeField]
    private Scroller _scroller;

    [SerializeField] private PlayerMover _playerMover;
        
    [SerializeField] private int levelDistance;
    [SerializeField] private float distanceTravelled;
    
    void Pause()
    {
        // OnGamePause?.Invoke(this);
    }

    void Unpause()
    {
        // OnGameUnpaused?.Invoke(this);
    }

    void Quit()
    {
        Application.Quit();    
    }
    
    void Update()
    {
        if (distanceTravelled >= levelDistance)
        {
            Debug.Log("round over");
            _scroller.RoundOver();
            _playerMover.RoundOver();
        }
        else
        {
            distanceTravelled += _scroller.scrollSpeed;
        }

    }

    private void Awake()
    {
        _scroller = FindObjectOfType<Scroller>();
        
        if (Instance != null)
        {
            Destroy(this);
        } else {
            Instance = this;
        } 
        DontDestroyOnLoad(gameObject);
    }
    
}
