using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class EatController : MonoBehaviour
{
    [SerializeField] private AnimationController _animation;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private Health _health;

    public readonly UnityEvent AttackEvent = new UnityEvent();
    public readonly UnityEvent<int> PointEvent = new UnityEvent<int>();

    private Health _enemyHealth;

    private int _damage;
    [SerializeField] private int _startdamage;
    [SerializeField] private float _percentDamageUpgrade;

    private void Awake()
    {
        _levelController.NewLevelEvent.AddListener(DamageUpgrade);
        _health.DeathEvent.AddListener(Dead);
        AttackEvent.AddListener(Attack);
    }
    private void Start()
    {
        _damage = _startdamage;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health enemy))
        {
            _enemyHealth = enemy;
        }
        if (other.TryGetComponent(out  PointObject _pointObject))
        {
            _animation.ActivateEatAnimation();
            PointEvent?.Invoke(_pointObject.GetPoints());
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Health enemy))
        {
            if (enemy = _enemyHealth)
                _enemyHealth = null;
        }
    }

    private void Attack()
    {
        if (_enemyHealth != null)
        {
            _enemyHealth.TakeDamage(_damage);
            _animation.ActivateAttackAnimation();
        }
    }
    private void DamageUpgrade(int Level)
    {
        float maxDamage = _startdamage;
        for (int i = 0; i < Level; i++)
        {
            maxDamage += (_percentDamageUpgrade / 100) * maxDamage;
        }
        _damage = (int)maxDamage;
    }
    
    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
