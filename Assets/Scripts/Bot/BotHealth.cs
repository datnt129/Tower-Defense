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

    //Temp var
    BotFX _botFx;
    //Temp var

    private void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;
        _bot = GetComponent<Bot>();

        //Temp var
        _botFx = GetComponent<BotFX>();
        //Temp var
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
            // _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
            _healthBar.fillAmount = Mathf.Clamp(CurrentHealth / maxHealth, 0, 1);

            //Temp var
            StartCoroutine(_botFx.BotHit(_bot, 5f));
            //Temp var        
        }
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
        Destroy(_healthBar);
        OnBotKilled?.Invoke(_bot);
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
    }
}
