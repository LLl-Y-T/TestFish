using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsObject : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private LevelController _levelController;

    [SerializeField] private PointObject pointObject;

    void Awake()
    {
        _health.DeathEvent.AddListener(StartSpawnFood);
    }

    private void StartSpawnFood()
    {
        PointObject point = Instantiate(pointObject);
        point.PointUpgrade(_levelController.GetLevel());
        point.transform.position = GetComponentInParent<Transform>().position;
    }
}
