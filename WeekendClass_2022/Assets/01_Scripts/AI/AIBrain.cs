using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _currentState;

    public Transform Target = null;

    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
    }

    public void ChangeToState(AIState nextState)
    {
        _currentState = nextState;
    }

    protected virtual void Update()
    {
        _currentState.UpdateState();
    }

    public virtual void Attack()
    {
        Debug.Log("공격!");
    }

    public void Move(Vector2 dir, Vector2 target)
    {
        Debug.Log("이동!");
    }
}
