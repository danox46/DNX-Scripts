using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_CharIsAble : BaseNode
{
    private Sweepbot thisChar;

    public C_CharIsAble(Sweepbot currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.CharIsAble() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
