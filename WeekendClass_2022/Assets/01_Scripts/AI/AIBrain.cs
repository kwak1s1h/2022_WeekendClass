using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackData
{
    public EnemyAttack atk;
    public Action<bool> action;
    public float coolTime;
}

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _currentState;
    private AIStateInfo _stateInfo;
    private AgentMovement _movement;

    public Transform Target = null;
    private Dictionary<SkillName, EnemyAttackData> _attackDictionary = new Dictionary<SkillName, EnemyAttackData>();

    public UnityEvent<Vector2> OnMovementCommand;
    
    private void Awake()
    {
        _stateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
        _movement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
        MakeAttackType();
    }

    private void MakeAttackType()
    {
        Transform atkTrm = transform.Find("AttackType");
        EnemyAttackData rangeAttack = new EnemyAttackData
        {
            atk = atkTrm.GetComponent<RangeAttack>(),
            action = (v) => {
                _stateInfo.IsAttack = false;
                _stateInfo.IsRange = false;
            },
            coolTime = 3f
        },
        meleeAttack = new EnemyAttackData
        {
            atk = atkTrm.GetComponent<MeleeAttack>(),
            action = (v) => {
                _stateInfo.IsAttack = false;
                _stateInfo.IsMelee = true;
            }
        };

        _attackDictionary.Add(SkillName.Range, rangeAttack);
    }

    public void ChangeToState(AIState nextState)
    {
        _currentState = nextState;
    }

    protected virtual void Update()
    {
        _currentState.UpdateState();

        if(_stateInfo.MeleeCool > 0)
        {
            _stateInfo.MeleeCool -= Time.deltaTime;
            if(_stateInfo.MeleeCool < 0) 
                _stateInfo.MeleeCool = 0;
        }
        if(_stateInfo.RangeCool > 0)
        {
            _stateInfo.RangeCool -= Time.deltaTime;
            if(_stateInfo.RangeCool < 0) 
                _stateInfo.RangeCool = 0;
        }
    }

    public virtual void Attack(SkillName skillname)
    {
        if(_stateInfo.IsAttack) {
            return;
        }

        EnemyAttackData attackData = null;
        FieldInfo fInfo = typeof(AIStateInfo).GetField(
            $"{skillname.ToString()}Cool",
            BindingFlags.Public | BindingFlags.Instance
        );

        if((float)fInfo.GetValue(_stateInfo) > 0)
        {
            return;
        }

        FieldInfo fInfoBool = typeof(AIStateInfo).GetField(
            $"Is{skillname.ToString()}",
            BindingFlags.Public | BindingFlags.Instance
        );

        if(_attackDictionary.TryGetValue(skillname, out attackData)) {
            _movement.StopImmediatlly();
            _stateInfo.IsAttack = true;
            fInfoBool.SetValue(_stateInfo, true);
            fInfo.SetValue(_stateInfo, attackData.coolTime);
            attackData.atk.Attack(attackData.action);
        }
    }

    public void Move(Vector2 dir, Vector2 target)
    {
        OnMovementCommand?.Invoke(dir);
    }
}
