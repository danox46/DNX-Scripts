using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using Dnx2DPlatfomerTools;

public class C_CharInPatrolPoint : BaseNode
{
    private Sweepbot thisChar;

    public C_CharInPatrolPoint(Sweepbot currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        if (thisChar.patrollingPoints.Count > 0)
        {
            m_state = Tools2DPlatfomer.TargetisInRangeHorizontal(thisChar.transform, thisChar.currentPatrolPoint(), Tools2DPlatfomer.CharMinimunDistance(thisChar.gameObject)) ? NodeState.SUCCESS : NodeState.FAILURE;
        }
        else
        {
            m_state = NodeState.FAILURE;
            Debug.LogError("No patrol points added to the patrolling enemy");
        }

        return m_state;
    }
}
