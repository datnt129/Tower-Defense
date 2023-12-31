using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] protected float delayBtwAttacks = 2f;
    [SerializeField] protected float damage = 2f;

    public float Damage { get; set; }

    public float DelayPerShot { get; set; }
    protected float _nextAttackTime;
    protected Turret _turret;
    protected Projectile _currentProjectileLoaded;

    private void Start()
    {
        _turret = GetComponent<Turret>();
        Damage = damage;
        DelayPerShot = delayBtwAttacks;
        LoadProjectile();
    }
    protected virtual void Update()
    {
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentBotTarget != null &&
                _currentProjectileLoaded != null &&
                _turret.CurrentBotTarget.BotHealth.CurrentHealth > 0f )
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetBot(_turret.CurrentBotTarget);
            }
            _nextAttackTime = Time.time + DelayPerShot;
        }
    }

    
    protected virtual void LoadProjectile()
    {
        GameObject newInstance = Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation, projectileSpawnPosition);

        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.Damage = Damage;
    }

    private bool IsTurretEmpty()
    {
        return ( _currentProjectileLoaded == null );
    }

    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }
}
