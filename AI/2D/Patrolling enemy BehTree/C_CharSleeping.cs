using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_CharSleeping : BaseNode
{
    private Sweepbot thisChar;

    public C_CharSleeping(Sweepbot character) : base()
    {
        thisChar = character;
    }

    public override NodeState Evaluate()
    {
        m_state = !thisChar.awake ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
