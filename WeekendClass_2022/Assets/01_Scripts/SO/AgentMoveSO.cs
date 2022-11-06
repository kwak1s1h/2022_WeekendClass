using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/Move")]
public class AgentMoveSO : ScriptableObject
{
    public float Accel = 50f;
    public float DeAccel = 50f;
    [Range(0.1f, 10f)] public float MaxSpeed = 10f;
}
