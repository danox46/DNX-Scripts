using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class T_EndWait : BaseNode
{
    private Sweepbot thisChar;

    public T_EndWait(Sweepbot currentbot) : base()
    {
        thisChar = currentbot;
    }

    public override NodeState Evaluate()
    {
        thisChar.ChangePatrolPoint();
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
