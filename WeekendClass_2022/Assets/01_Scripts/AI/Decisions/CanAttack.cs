using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttack : AIDecision
{
    public SkillName Skill;

    public override bool MakeADecision()
    {
        FieldInfo fInfo = typeof(AIStateInfo).GetField(
            $"{Skill.ToString()}Cool", 
            BindingFlags.Public | BindingFlags.Instance
        );
        float coolTime = (float)fInfo.GetValue(_state);

        return !_state.IsAttack && coolTime <= 0;
    }
}
