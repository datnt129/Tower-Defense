using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    CircleCollider2D circleCollider;
    List<Bot> _bots;
    public Bot CurrentBotTarget;
    
    public List<Bot> bots => _bots;
    public float TurretRange;

    void Start()
    {
        _bots = new List<Bot>();
        circleCollider = GetComponent<CircleCollider2D>();

        circleCollider.radius = TurretRange;
    }

    private void Update()
    {
        GetCurrentBotTarget();
        RotateTowardsTarget();
    }

    private void GetCurrentBotTarget()
    {
        if (_bots.Count <= 0)
        {
            CurrentBotTarget = null;
            return;
        }
        CurrentBotTarget = _bots[0];
    }

    private void RotateTowardsTarget()
    {
        if (CurrentBotTarget == null)
        {
            return;
        }
        Vector3 targetPos = CurrentBotTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);

        transform.Rotate(0f, 0f, angle);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Bot"))
        {
            Bot newBot = obj.GetComponent<Bot>();
            _bots.Add(newBot);
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Bot"))
        {
            Bot bot = obj.GetComponent<Bot>();
            if (_bots.Contains(bot))
            {
                bots.Remove(bot);
            }
        }
    }
}