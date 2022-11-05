using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    // 내가 {클래스 이름} 상태에서 수행해야 할 액션들을 가진 리스트
    public List<AIAction> actions = null;
    // 내가 {클래스 이름} 상태에서 할 수 있는 트랜지션들
    public List<AITransition> transitions = null;

    private AIBrain _brain;

    private void Awake()
    {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public void UpdateState()
    {
        foreach(AIAction action in actions)
        {
            action.TakeAction();
        }

        foreach(AITransition transition in transitions)
        {
            bool result = false;
            foreach(AIDecision decision in transition.decisions)
            {
                result = decision.MakeADecision();
                if(!result) break;
            }

            if(result)
            {
                if(transition.positiveResult != null)
                {
                    _brain.ChangeToState(transition.positiveResult);
                }
            }
            else
            {
                if(transition.negativeResult != null)
                {
                    _brain.ChangeToState(transition.negativeResult);
                }
            }
        }
    }
}
