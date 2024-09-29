using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    public static readonly UnityEvent<int> PointEvent = new UnityEvent<int>();

    public static void PointEventInvoke(int point)
    {
        PointEvent.Invoke(point);
    }
}
