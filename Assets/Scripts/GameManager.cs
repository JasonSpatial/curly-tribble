using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { Starting, Started, Paused, RoundOver, GameOver }
public class GameManager : MonoBehaviour
{

    // TODO
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

    [SerializeField] public GameStates _gameState;

    [SerializeField] private int rounds;
    [SerializeField] private int currentRound;
    
    public Canvas startScreen;
    public Canvas pauseScreen;
    public Canvas roundEndScreen;
    public Canvas gameOverScreen;
    
    void StartGame()
    {
        rounds = 1;
        currentRound = 1;
        _gameState = GameStates.Starting;
        // startScreen.enabled = true;
        startScreen.gameObject.SetActive(true);
        // show start screen
    }

    public void StartRound()
    {
        startScreen.gameObject.SetActive(false);
        _gameState = GameStates.Started;
    }

    public void NextRound()
    {
        currentRound++;
        StartRound();
    }

    public void PlayAgain()
    {
        StartGame();
    }
    public void Pause()
    {
        _gameState = GameStates.Paused;
        pauseScreen.gameObject.SetActive(true);
        // show pause menu
        // OnGamePause?.Invoke(this);
    }

    public void Unpause()
    {
        pauseScreen.gameObject.SetActive(false);
        _gameState = GameStates.Started;
        // hide pause menu
        // OnGameUnpaused?.Invoke(this);
    }

    public void EndRound()
    {
        _gameState = GameStates.RoundOver;
        roundEndScreen.gameObject.SetActive(true);

    }

    public void GameOver()
    {
        _gameState = GameStates.GameOver;
        gameOverScreen.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();    
    }
    
    void Update()
    {

        
        if (distanceTravelled >= levelDistance)
        {
            Debug.Log("round over");
            if (currentRound == rounds)
            {
                GameOver();
            }
            else
            {
                EndRound();
            }
            // _scroller.RoundOver();
            // _playerMover.RoundOver();
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

    private void Start()
    {
        StartGame();
    }

    void OnPause()
    {
        if (_gameState == GameStates.Started)
        {
            Pause();
        }
    }

    void OnUnpause()
    {
        if (_gameState == GameStates.Paused)
        {
            Unpause();
        }
    }
}
