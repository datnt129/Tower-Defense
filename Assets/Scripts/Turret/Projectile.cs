using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Bot, float> OnBotHit;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] private float minDistanceToDealDamage = 0.1f;

    public TurretProjectile TurretOwner { get; set; }

    public float Damage { get; set; }
    protected Bot _botTarget;

    protected virtual void Update()
    {
        if (_botTarget != null)
    {
            MoveProjectile();
            RotateProjectile();
        }
    }

    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            _botTarget.transform.position, moveSpeed * Time.deltaTime
        );

        float distanceToTarget = (_botTarget.transform.position - transform.position).magnitude;

        if (distanceToTarget < minDistanceToDealDamage)
        {
            OnBotHit?.Invoke(_botTarget, Damage);
            _botTarget.BotHealth.DealDamage(Damage);
            TurretOwner.ResetTurretProjectile();
            Destroy(gameObject);
        }
    }

    private void RotateProjectile()
    {
        Vector3 botPos = _botTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, botPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void SetBot(Bot bot)
    {
        _botTarget = bot;
    }
}


