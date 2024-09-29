using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour
{
    [SerializeField] private int Points ;
    public void PointUpgrade(int Level)
    {
        Points = 100 + (Level * 50);
    }
    public int GetPoints()
    {
        Destroy(gameObject);
        return Points;
    }
}
