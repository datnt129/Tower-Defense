using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public static Action<Bot> OnEndReached;
    [SerializeField] private float InittialSpeed;
    private float speed;
    public Waypoint Waypoint;
    int _currentWaypointIndex;
    Vector3 _currentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);
    Vector3 CurrentPointPosition => _currentPointPosition;
    Vector3 _lastPointPosition;
    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rb2D;

    BotHealth _botHealth;

    public BotHealth BotHealth => _botHealth;    

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        _botHealth = GetComponent<BotHealth>();
        
        _currentWaypointIndex = 0;
        speed = InittialSpeed;
        
        _lastPointPosition = CurrentPointPosition;
    }

    void Update()
    {
        Move();
        Rotate();
        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, speed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;

        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _botHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void StopMovement()
    {
        speed = 0;
    }

    public void ResumeMovement()
    {
        speed = InittialSpeed;
    }
}
