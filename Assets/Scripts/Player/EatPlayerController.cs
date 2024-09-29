using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPlayerController : EatController
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackEvent?.Invoke();
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.TryGetComponent(out OutlineController enemy))
        {
            enemy.OutlineActivate(true);
        }
    }
    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.TryGetComponent(out OutlineController enemy))
        {
            enemy.OutlineActivate(false);
        }
    }
}
