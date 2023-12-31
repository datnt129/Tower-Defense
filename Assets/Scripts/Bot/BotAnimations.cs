using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimations : MonoBehaviour
{
    // public GameObject deathParticles;
    private Animator _animator;
    private Bot _bot;
    private BotHealth _botHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _bot = GetComponent<Bot>();
        _botHealth = GetComponent<BotHealth>();
    }
    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }

    private float GetCurrentAnimationLenght()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }

    private IEnumerator PlayHurt()
    {
        _bot.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _bot.ResumeMovement();
    }
    private IEnumerator PlayDead()
    {
        _bot.StopMovement();
        // Instantiate(deathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void BotHit(Bot bot)
    {
        if (_bot == bot)
        {
            StartCoroutine(PlayHurt());
        }
    }


    private void BotDead(Bot bot)
    {
        if (_bot == bot)
        {
            StartCoroutine(PlayDead());
        }
    }

    private void OnEnable()
    {
        BotHealth.OnBotHit += BotHit;
        BotHealth.OnBotKilled += BotDead;
    }

    private void OnDisable()
    {
        BotHealth.OnBotHit -= BotHit;
        BotHealth.OnBotKilled -= BotDead;
    }
}
