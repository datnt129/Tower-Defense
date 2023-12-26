
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BotFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;
    [SerializeField] private GameObject dmgTextPrefab;
    private Bot _bot;
    public static Action<Transform, float> onHit;

    private void Start()
    {
        _bot = GetComponent<Bot>();
    }
    
    public void BotHit(Bot bot, float damage)
    {
        if (_bot == bot)
        {
            onHit?.Invoke(textDamageSpawnPosition, damage);
        }
    }

    private void OnEnable()
    {
        Projectile.OnBotHit += BotHit;
    }

    private void OnDisable()
    {
        Projectile.OnBotHit -= BotHit;
    }
}
