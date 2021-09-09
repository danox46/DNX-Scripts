using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using Dnx2DPlatfomerTools;

public class T_MoveToTarget : BaseNode
{
    private Sweepbot thisChar;

    public T_MoveToTarget(Sweepbot currentbot) : base()
    {
        thisChar = currentbot;
    }

    public override NodeState Evaluate()
    {
        thisChar.Move(Tools2DPlatfomer.DirectionToTargetHorizontal(thisChar.transform, thisChar.target), false, false);
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
