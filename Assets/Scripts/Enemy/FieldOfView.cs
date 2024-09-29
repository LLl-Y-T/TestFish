using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    public readonly UnityEvent<Vector3> AgresivEnemyAttackEvent = new UnityEvent<Vector3>();
    public readonly UnityEvent<Transform> FoodEvent = new UnityEvent<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out LevelController otherEnemy))
        {
            if (_levelController.GetLevel() <= otherEnemy.GetLevel())
                AgresivEnemyAttackEvent?.Invoke(otherEnemy.transform.position);
        }
        else if(other.TryGetComponent(out PointObject pointObject))
        {
            FoodEvent?.Invoke(pointObject.transform);
        }
    }
}
