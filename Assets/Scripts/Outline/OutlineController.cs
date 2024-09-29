using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private Outline _outline;
    public void OutlineActivate(bool active)
    {
        if (active)
            _outline.OutlineWidth = 5;
        else
            _outline.OutlineWidth = 0;
    }
}
