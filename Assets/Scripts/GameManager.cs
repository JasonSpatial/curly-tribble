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

    [SerializeField] private Destination _destination;
    [SerializeField]
    private Scroller _scroller;

    [SerializeField] private PlayerMover _playerMover;
        
    [SerializeField] public int levelDistance;
    [SerializeField] private float distanceTravelled;
    [SerializeField] public float distanceRemaining;

    [SerializeField] public GameStates _gameState;

    [SerializeField] private int rounds;
    [SerializeField] private int currentRound;
    
    public Canvas startScreen;
    public Canvas pauseScreen;
    public Canvas roundEndScreen;
    public Canvas gameOverScreen;

    //Audio Variables
    private Camera cam => Camera.main;
    private bool isPlaying;
    
    void StartGame()
    {
        PlayMusic();
        distanceTravelled = 0;
        _destination.Reset();
        rounds = 1;
        currentRound = 1;
        _gameState = GameStates.Starting;
        // startScreen.enabled = true;
        startScreen.gameObject.SetActive(true);
        // show start screen
    }

    private void PlayMusic()
    {
        //Start music, but only on the first playthrough
        if(!isPlaying && cam)
        {
            AkSoundEngine.PostEvent("Play_MainMusic", cam.gameObject);
            AkSoundEngine.PostEvent("Play_Engine", cam.gameObject);
            isPlaying = true;
        }
    }

    public void StartRound()
    {
        //Play button sound
        if (cam) { AkSoundEngine.PostEvent("Play_ButtonPress", cam.gameObject); }

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
        //Play button sound
        if (cam) { AkSoundEngine.PostEvent("Play_ButtonPress", cam.gameObject); }
        
        roundEndScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        StartGame();
    }
    public void Pause()
    {
        //Play sound and lower music and engine volume
        if (cam) { AkSoundEngine.PostEvent("Play_QButtonPress", cam.gameObject); }
        AkSoundEngine.SetRTPCValue("GamePause", 100f);

        _gameState = GameStates.Paused;
        pauseScreen.gameObject.SetActive(true);
        // show pause menu
        // OnGamePause?.Invoke(this);
    }

    public void Unpause()
    {
        //Play sound and normalize music and engine volume
        if (cam) { AkSoundEngine.PostEvent("Play_ButtonPress", cam.gameObject); }
        AkSoundEngine.SetRTPCValue("GamePause", 0f);

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
        if (_gameState == GameStates.Started)
        {
            distanceRemaining = levelDistance - distanceTravelled;
            
            if (distanceRemaining <= 0)
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
