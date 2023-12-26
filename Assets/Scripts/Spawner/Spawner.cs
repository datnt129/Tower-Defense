using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int botCount = 10;
    [SerializeField]
    private GameObject testGO;
    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;
    private float _spawnTimer;
    private int _botsSpawned;
    private ObjectPooler _pooler;
    private Waypoint _waypoint;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
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
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.position = transform.position;
        Bot bot = newInstance.GetComponent<Bot>();
        bot.Waypoint = _waypoint;
        newInstance.SetActive(true);
    }
}


