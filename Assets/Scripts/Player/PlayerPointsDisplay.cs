using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPointsDisplay : MonoBehaviour
{
    [SerializeField] private SliederController _pointBar;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private LevelController _levelController;
    private void Awake()
    {
        _levelController.PointsToUpLevelEvent.AddListener(MaxValuePointBarUpdate);
        _levelController.CurrentPointsEvent.AddListener(ValuePointBarUpdate);
        _levelController.NewLevelEvent.AddListener(LevelTextUpdate);
    }
    private void MaxValuePointBarUpdate(int pointsToUpLevel)
    {
        _pointBar.UpdateMaxValue(pointsToUpLevel);
    }
    private void LevelTextUpdate(int level)
    {
        _levelText.text = level.ToString() + ":LVL";
    }
    private void ValuePointBarUpdate(int currentPoints)
    {
        _pointBar.UpdateSlider(currentPoints);
    }
}
