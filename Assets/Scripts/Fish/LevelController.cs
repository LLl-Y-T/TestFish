using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    [SerializeField] private EatController _eatController;

    public readonly UnityEvent<int> PointsToUpLevelEvent = new UnityEvent<int>();
    public readonly UnityEvent<int> CurrentPointsEvent = new UnityEvent<int>();
    public readonly UnityEvent<int> NewLevelEvent = new UnityEvent<int>();

    private int _level = 0;
    private float _rateLevelComplication = 1.5f;
    private float _rateGrowthIncrease = 1.5f;

    public int _pointsToUpLevel = 100;
    public int _currentPoints;

    private void Awake()
    {
        _eatController.PointEvent.AddListener(addPoints);
    }
    private void Start()
    {
        StartNewLevelEvent();
    }
    private void addPoints(int points)
    {
        _currentPoints += points;
        while (_currentPoints >= _pointsToUpLevel)
        {
            _currentPoints -= _pointsToUpLevel;
            LevelUpgrade();
        }
        CurrentPointsEvent?.Invoke(_currentPoints);
    }
    private void LevelUpgrade()
    {
        _level++;
        _pointsToUpLevel = (int)(_pointsToUpLevel * _rateLevelComplication);
        transform.localScale *= _rateGrowthIncrease;

        StartNewLevelEvent();
    }

    private void StartNewLevelEvent()
    {
        NewLevelEvent?.Invoke(_level);
        PointsToUpLevelEvent?.Invoke(_pointsToUpLevel);
    }

    public int GetLevel()
    {
        return _level;
    }
}
