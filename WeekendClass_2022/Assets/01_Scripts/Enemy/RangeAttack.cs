using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : EnemyAttack
{
    [SerializeField] private float _playTime = 1f;
    private SpriteRenderer _spriteRenderer;

    private Action<bool> callback;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = _brain.GetComponent<SpriteRenderer>();
    }

    public override void Attack(Action<bool> callback)
    {
        _spriteRenderer.color = Color.red;
        this.callback = callback;
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_playTime);
        _spriteRenderer.color = Color.white;
        callback.Invoke(true);
    }
}
