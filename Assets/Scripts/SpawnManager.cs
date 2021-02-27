using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<Obstacle> asteroids;
    public List<Obstacle> nonAsteroids;
    public List<Pickup> pickups;

    [SerializeField]
    private Transform _top, _bottom;
    private int _roundDistance;

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
        Instantiate(asteroidToSpawn, asteroidSpawnPosition, Quaternion.identity);
    }
    void SpawnNonAsteroid()
    {
        // Debug.Log("spawn non asteroid");
    }
    void SpawnPickup()
    {
        var pickupToSpawn = pickups[Random.Range(0, pickups.Count)];
        Vector2 pickupSpawnPosition = new Vector2(gameObject.transform.position.x, Random.Range(_bottom.position.y, _top.position.y));
        Instantiate(pickupToSpawn, pickupSpawnPosition, Quaternion.identity);
    }
    
    void Update()
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
