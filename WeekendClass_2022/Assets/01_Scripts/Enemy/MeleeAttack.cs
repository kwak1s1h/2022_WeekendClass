using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    [SerializeField] private float _playTime = 0.8f;
    private SpriteRenderer _spriteRenderer;

    private Action<bool> callback;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = _brain.GetComponent<SpriteRenderer>();
    }

    public override void Attack(Action<bool> callback)
    {
        _spriteRenderer.color = Color.green;
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
