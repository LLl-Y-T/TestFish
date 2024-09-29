using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore;

public class MoveController : MonoBehaviour
{
    [SerializeField] private SliederController _stamineBar;
    [SerializeField] private AnimationController _animationController;

    private Rigidbody _rb;
    Vector3 moveDirection = new Vector3();

    private float _sensitivity = 300;
    private float _yRotation = 0f;
    private float _xRotation = 0f;

    [SerializeField] private float _speedDef = 40;

    [SerializeField] private float _staminaMax = 100;

    [SerializeField] private float _speedRate = 2;

    [SerializeField] private float _staminaRecoveryRate = 18;

    private float _speed = 40;
    private float _stamina = 100;
    private float _stamina—onsumption = 30;

    private float _timeToRecoveryStamina = 3;
    private float _currentTimeToRecoveryStamina = 3;

    private bool _obstacle;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        _speed = _speedDef;
    }

    void FixedUpdate()
    {
        RotationLogic();
        MovementLogic();
        FastRunLogic();
    }

    private void RotationLogic()
    {
        _yRotation += Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        _xRotation -= Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

       // _xRotation = Mathf.Clamp(_xRotation, -90f, 30);

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
    private void MovementLogic()
    {
        float moveVertical = Input.GetAxis("Vertical");
        CheckObstacle();

        if (moveVertical != 0 && !_obstacle)
        {
            _animationController.ActivateRunAnimation(true);
            moveDirection = transform.forward * moveVertical;
            Move();
        }
        else
        {
            _animationController.ActivateRunAnimation(false);
        }
    }
    private void CheckObstacle()
    {
        int _checkObstacleDistance = 1;
        int layerMask = 1 << 6;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, _checkObstacleDistance, layerMask);
        if (hit.collider != null)
        {
            _obstacle = true;
        }
        else
        {
            _obstacle = false;
        }
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + moveDirection * _speed * Time.deltaTime);
    }

    private void FastRunLogic()
    {
        if (Input.GetAxis("Jump") > 0 && _stamina>0)
        {

            _animationController.ActivateFastRunAnimation(true);
            _speed = _speedDef * _speedRate;
            _stamina -= _stamina—onsumption * Time.deltaTime;
            _currentTimeToRecoveryStamina = _timeToRecoveryStamina;
        }
        else
        {
            _animationController.ActivateFastRunAnimation(false);
            _speed = _speedDef;
            _currentTimeToRecoveryStamina -= Time.deltaTime;

            if (_currentTimeToRecoveryStamina <= 0) 
                _stamina += _staminaRecoveryRate * Time.deltaTime;
        }

        _stamineBar.UpdateSlider(_stamina);
    }

}
