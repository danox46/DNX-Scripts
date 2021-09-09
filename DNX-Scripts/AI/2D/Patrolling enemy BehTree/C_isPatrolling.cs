using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_isPatrolling : BaseNode
{
    private Sweepbot thisChar;

    public C_isPatrolling(Sweepbot currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.isPatrolChar ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }

}
