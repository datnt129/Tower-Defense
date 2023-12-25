using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;

public class BotFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;
    [SerializeField] private GameObject dmgTextPrefab;
    private Bot _bot;

    private void Start()
    {
        _bot = GetComponent<Bot>();
    }
    public IEnumerator BotHit(Bot bot, float damage)
    {
        print("Bot Hitting");
        if (_bot == bot)
        {
            // GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
            GameObject newInstance = Instantiate(dmgTextPrefab, textDamageSpawnPosition.position, Quaternion.identity);

            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            // damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpawnPosition);
            // newInstance.transform.position = textDamageSpawnPosition.position;
            // newInstance.SetActive(true);
            yield return new WaitForSeconds(1f);
            Destroy(newInstance);
        }
    }
    private void OnEnable()
    {
        // Projectile.OnBotHit += BotHit;
    }
    private void OnDisable()
    {
        // Projectile.OnBotHit -= BotHit;
    }
}
