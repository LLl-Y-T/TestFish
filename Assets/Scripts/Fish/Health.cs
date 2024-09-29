using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private int _health;
    private int _maxHealth;

    private bool _dead;

    public readonly UnityEvent DeathEvent = new UnityEvent();
    [SerializeField] private ParticleSystem bloodSystem;

    [SerializeField] private AnimationController _animation;
    [SerializeField] private LevelController _levelController;

    [SerializeField]private int _startHealth = 100;
    [SerializeField]private float _percentHealthUpgrade = 30;

    [SerializeField]private float _timeToRecoveryHealth = 10;
    [SerializeField]private float _percentRecoveryHealth = 10;

    private Coroutine _healthRecoveryCoroutine;

    private void Awake()
    {
        _animation.DeathEvent.AddListener(DestroyObject);
        _levelController.NewLevelEvent.AddListener(HealthUpgrade);
    }
    void Start()
    {
        _maxHealth = _startHealth;
        _health = _maxHealth;
    }
    private IEnumerator HealthRecoveryCoroutine()
    {
        float stepTime = 0.1f;
        float CurrentTimeToRecoveryHealth = 0;
        while (_health < _maxHealth) 
        {
            if (CurrentTimeToRecoveryHealth >= _timeToRecoveryHealth)
                HealthRecovery((int)(_percentRecoveryHealth * _maxHealth / 100));
            else
                CurrentTimeToRecoveryHealth += stepTime;

            yield return new WaitForSeconds(stepTime);
        }

        if(_healthRecoveryCoroutine!=null)
            StopCoroutine( _healthRecoveryCoroutine );
    }
    private void HealthRecovery(int Health)
    {
        _health += Health;
        if (_health > _maxHealth)
            _health = _maxHealth;
    }
    private void StartHealthRecovery()
    {
        if (_healthRecoveryCoroutine != null)
            StopCoroutine(_healthRecoveryCoroutine);

        _healthRecoveryCoroutine = StartCoroutine(HealthRecoveryCoroutine());
    }

    public void TakeDamage(int damage)
    {
        if (!_dead)
        {
            if (_health < 0)
            {
                Death();
            }
            else
            {
                _health -= damage;
                bloodSystem.Play();
                StartHealthRecovery();
            }
        }
    }
    private void Death()
    {
        _dead = true;
        _animation.ActivateDeadAnimation();
        DeathEvent?.Invoke();

        if (_healthRecoveryCoroutine != null)
            StopCoroutine(_healthRecoveryCoroutine);
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    private void HealthUpgrade(int Level)
    {
        float maxHealth = _startHealth;
        for(int i = 0; i < Level; i++)
        {
            maxHealth += (_percentHealthUpgrade / 100) * maxHealth;
        }
        _maxHealth = (int)maxHealth;
    }
    public int GetHealth()
    {
        return _health;
    }
}
