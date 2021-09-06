using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_CharCloseToPatrolPoint : BaseNode
{
    private NPC thisChar;

    public C_CharCloseToPatrolPoint(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
    
        if(Vector3.Distance(thisChar.transform.position, thisChar.currentPatrolPoint.position) < 0.5f)
        {
            m_state = NodeState.SUCCESS;
        }
        else
        {
            m_state = NodeState.FAILURE;
        }

        return m_state;
    }
}
