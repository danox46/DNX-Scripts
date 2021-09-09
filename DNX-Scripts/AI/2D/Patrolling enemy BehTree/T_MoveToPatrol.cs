using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using Dnx2DPlatfomerTools;

public class T_MoveToPatrol : BaseNode
{
    private Sweepbot thisChar;

    public T_MoveToPatrol(Sweepbot currentbot) : base()
    {
        thisChar = currentbot;
    }

    public override NodeState Evaluate()
    {
        thisChar.Move(Tools2DPlatfomer.DirectionToTargetHorizontal(thisChar.transform, thisChar.currentPatrolPoint()), false, false);
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
