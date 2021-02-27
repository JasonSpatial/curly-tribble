using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    public List<Obstacle> asteroids;
    public List<Obstacle> nonAsteroids;
    public List<Pickup> pickups;

    [SerializeField]
    private Transform _top, _bottom;
    private int _roundDistance;
    public PlayerManager _playerManager;
    private float _boostSpeed = 0;
    
    [SerializeField] private float asteroidStart, asteroidEnd, nonAsteroidStart, nonAsteroidEnd, pickupStart, pickupEnd;
    private float _timeToNextAsteroidSpawn, _timeToNextNonAsteroidSpawn, _timeToNextPickupSpawn;
    void Start()
    {
        _timeToNextAsteroidSpawn = Random.Range(asteroidStart, asteroidEnd);
        _timeToNextNonAsteroidSpawn = Random.Range(nonAsteroidStart, nonAsteroidEnd);
        _timeToNextPickupSpawn = Random.Range(pickupStart, pickupEnd);
        
    }

    
    void SpawnAsteroid()
    {
        var asteroidToSpawn = asteroids[Random.Range(0, asteroids.Count)];
        Vector2 asteroidSpawnPosition = new Vector2(gameObject.transform.position.x, Random.Range(_bottom.position.y, _top.position.y));
        var asteroid = Instantiate(asteroidToSpawn, asteroidSpawnPosition, Quaternion.identity);
        asteroid.GetComponent<PickupMover>().moveSpeed = Random.Range(-2f, -0.5f) + _boostSpeed;
    }
    void SpawnNonAsteroid()
    {
        // Debug.Log("spawn non asteroid");
    }
    void SpawnPickup()
    {
        var pickupToSpawn = pickups[Random.Range(0, pickups.Count)];
        Vector2 pickupSpawnPosition = new Vector2(gameObject.transform.position.x, Random.Range(_bottom.position.y, _top.position.y));
        var pickup = Instantiate(pickupToSpawn, pickupSpawnPosition, Quaternion.identity);
        pickup.GetComponent<PickupMover>().moveSpeed = Random.Range(-2f, -0.5f) + _boostSpeed;
        
    }

    void ActivateBoost(PlayerManager playerManager)
    {
        _boostSpeed = playerManager.boostSpeed;
    }

    void DeactivateBoost(PlayerManager _)
    {
        _boostSpeed = 0;
    }
    
    void Update()
    {
        if (GameManager.Instance._gameState == GameStates.Started)
        {
            if(_timeToNextAsteroidSpawn <= 0f)
            {
                SpawnAsteroid();
                _timeToNextAsteroidSpawn = Random.Range(asteroidStart, asteroidEnd);
            }
            else
            {
                _timeToNextAsteroidSpawn -= Time.deltaTime;
            }
            
            if(_timeToNextNonAsteroidSpawn <= 0f)
            {
                SpawnNonAsteroid();
                _timeToNextNonAsteroidSpawn = Random.Range(nonAsteroidStart, nonAsteroidEnd);
            }
            else
            {
                _timeToNextNonAsteroidSpawn -= Time.deltaTime;
            }
                    
            if(_timeToNextPickupSpawn <= 0f)
            {
                SpawnPickup();
                _timeToNextPickupSpawn = Random.Range(pickupStart, pickupEnd);
            }
            else
            {
                _timeToNextPickupSpawn -= Time.deltaTime;
            }
        }
    }

    private void OnEnable()
    { 
        _playerManager.OnStartBoost += ActivateBoost;
        _playerManager.OnEndBoost += DeactivateBoost;
    }
}
