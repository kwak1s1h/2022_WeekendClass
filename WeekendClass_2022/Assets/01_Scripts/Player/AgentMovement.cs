using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;

    protected float _currentVelocity = 0;
    protected Vector2 _movementDirection;

    [SerializeField] private AgentMoveSO _moveSO;

    protected void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            if(Vector2.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalcSpeed(movementInput);
    }

    private float CalcSpeed(Vector2 movementInput)
    {
        if(movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += _moveSO.Accel * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _moveSO.DeAccel * Time.deltaTime;
        }
        
        return Mathf.Clamp(_currentVelocity, 0, _moveSO.MaxSpeed);
    }

    public void StopImmediatlly()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
    }
}
