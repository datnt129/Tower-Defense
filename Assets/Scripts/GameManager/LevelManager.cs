using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 5;

    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }
    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void ReduceLives(Bot bot)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void WaveCompleted()
    {
        
    }

    private void OnEnable()
    {
        Bot.OnEndReached += ReduceLives;
        //Spawner.OnWaveCompleted += WaveCompleted;
    }
    private void OnDisable()
    {
        Bot.OnEndReached -= ReduceLives;
    }
}
