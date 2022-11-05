using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 dir = _brain.Target.position - transform.position;

        _brain.Move(dir.normalized, _brain.Target.position);
    }
}
