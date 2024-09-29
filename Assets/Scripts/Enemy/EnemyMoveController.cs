using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private Health _health ;

    [SerializeField] private float _speedDef = 40;
    private float _speed = 40;

    [SerializeField] private float _speedRate = 3;
    [SerializeField] private float _fastRunningTime = 3;

    private bool _danger, _dead;
    [SerializeField] private Transform _targetObjectPosition;
    [SerializeField] private float _persecutionTime = 5f;

    private float _rotationSpeed = 5f;
    private float _timeToNextDirection = 5f;

    private Rigidbody _rb;
    private Quaternion _targetDirection;

    private Coroutine _nextDirectionCoroutine, _fastRunCoroutine, _persecutionCoroutine;

    private float _checkObstacleDistance = 5f;

    private void Awake()
    {
        _fieldOfView.AgresivEnemyAttackEvent.AddListener(AgresivEnemyAttack);
        _fieldOfView.FoodEvent.AddListener(RunningForFood);

        _health.DeathEvent.AddListener(Death);
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        _speed = _speedDef;

        _nextDirectionCoroutine = StartCoroutine(NextDirectionCoroutine());
    }

    void FixedUpdate()
    {
        if (!_dead)
        {
            RotationLogic();
            MovementLogic();
        }
    }

    private void RotationLogic()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetDirection, _rotationSpeed * Time.deltaTime);
    }
    private void MovementLogic()
    {
        _animationController.ActivateRunAnimation(true);

       CheckObstacle();
        _rb.MovePosition(_rb.position + transform.forward * _speed * Time.deltaTime);
    }
    private void CheckObstacle()
    {
        int layerMask = 1 << 6;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, _checkObstacleDistance, layerMask);
        if (hit.collider != null)
        {
            NewRandomDirection();
        }
    }
    private IEnumerator NextDirectionCoroutine()
    {
        while (true) 
        {
            _speed = _speedDef;
            NewRandomDirection();
            yield return new WaitForSeconds(_timeToNextDirection);
        }
    }

    private void NewRandomDirection()
    {
        float randomY = UnityEngine.Random.Range(0f, 360f);
        float randomX = UnityEngine.Random.Range(0f, 360f);
        float randomZ = 0;

        _targetDirection = Quaternion.Euler(randomX, randomY, randomZ);
    }
 
    private void AgresivEnemyAttack(Vector3 OtherEnemyPos)
    {
        if (_nextDirectionCoroutine != null)
            StopCoroutine(_nextDirectionCoroutine);


        Vector3 directionToTarget = OtherEnemyPos - transform.position;
        Vector3 oppositeDirection = -directionToTarget;
        _targetDirection = Quaternion.LookRotation(oppositeDirection);

        _fastRunCoroutine = StartCoroutine(FastRunningCorountine());
        _danger = true; 
    }

    private void RunningForFood(Transform FoodPos)
    {
        if (!_danger && _persecutionCoroutine == null)
        {
            _targetObjectPosition = FoodPos;

            _persecutionCoroutine = StartCoroutine(PersecutionCorountine());
            _fastRunCoroutine = StartCoroutine(FastRunningCorountine());
        }
    }
    private IEnumerator PersecutionCorountine()
    {
        float timeStep = 0.1f;
        float currentTimePersecution = 0;
        while (_targetObjectPosition!=null && currentTimePersecution < _persecutionTime)
        {
            currentTimePersecution += timeStep;

            if (_nextDirectionCoroutine != null)
                StopCoroutine(_nextDirectionCoroutine);

            Vector3 direction = _targetObjectPosition.position - transform.position;
            _targetDirection = Quaternion.LookRotation(direction);

            yield return new WaitForSeconds(timeStep);
        }

        if(_persecutionCoroutine != null)
            StopCoroutine(_persecutionCoroutine);

        if(_nextDirectionCoroutine == null)
            _nextDirectionCoroutine = StartCoroutine(NextDirectionCoroutine());
    }
    private IEnumerator FastRunningCorountine()
    {
        _animationController.ActivateFastRunAnimation(true);
        _speed = _speedDef * _speedRate;

        yield return new WaitForSeconds(_fastRunningTime);

        _danger = false;

        _animationController.ActivateFastRunAnimation(false);
        _speed = _speedDef;

        if (_fastRunCoroutine != null)
            StopCoroutine(_fastRunCoroutine);

        if(_nextDirectionCoroutine == null)
            _nextDirectionCoroutine = StartCoroutine(NextDirectionCoroutine());
    }

    private void Death()
    {
        _dead = true;
    }
}
