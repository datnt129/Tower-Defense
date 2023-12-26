using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotHealth : MonoBehaviour
{
    public static Action<Bot> OnBotKilled;
    public static Action<Bot> OnBotHit;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }
    private Image _healthBar;
    private Bot _bot;

    private void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;
        _bot = GetComponent<Bot>();
    }

    private void Update()
    {
        _healthBar.fillAmount = Mathf.Clamp(CurrentHealth / maxHealth, 0, 1);
    }
    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);
        BotHealthContainer container = newBar.GetComponent<BotHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
            OnBotHit?.Invoke(_bot);
    }

    private void Die()
    {
        OnBotKilled?.Invoke(_bot);
        ResetHealth();
        _bot.ResumeMovement();
        gameObject.SetActive(false);
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
    }
}
