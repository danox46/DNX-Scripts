using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class T_Attack : BaseNode
{
    private Sweepbot thisChar;

    public T_Attack(Sweepbot currentbot) : base()
    {
        thisChar = currentbot;
    }

    public override NodeState Evaluate()
    {
        thisChar.TriggerAttacking();
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
