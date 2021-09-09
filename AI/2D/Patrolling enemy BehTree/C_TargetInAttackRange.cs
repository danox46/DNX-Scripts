using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using Dnx2DPlatfomerTools;

public class C_TargetInAttackRange : BaseNode
{
    private Sweepbot thisChar;

    public C_TargetInAttackRange(Sweepbot currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        if (thisChar.target != null)
        {
            m_state = Tools2DPlatfomer.TargetisInRangeHorizontal(thisChar.transform, thisChar.target, thisChar.attackRange) ? NodeState.SUCCESS : NodeState.FAILURE;
        }
        else
        {
            m_state = NodeState.FAILURE;
        }

        return m_state;
    }
}
