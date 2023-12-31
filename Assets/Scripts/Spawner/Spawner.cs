using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int botCount = 10;
    [SerializeField]
    private GameObject gameObj;
    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;
    private float _spawnTimer;
    private int _botsSpawned;
    private Waypoint _waypoint;

    private void Start()
    {
        _waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime; if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (_botsSpawned < botCount)
            {
                _botsSpawned++;
                SpawnBot();
            }
        }
    }
    
    private void SpawnBot()
    {
        GameObject newInstance = Instantiate(gameObj, transform.position ,Quaternion.identity);

        Bot bot = newInstance.GetComponent<Bot>();
        bot.Waypoint = _waypoint;
    }
}


