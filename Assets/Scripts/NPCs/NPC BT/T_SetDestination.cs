using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class T_SetDestination : BaseNode
{
    private NPC thisChar;

    public T_SetDestination(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        thisChar.EndWait();
        thisChar.m_NavMesh.SetDestination(thisChar.currentPatrolPoint.position);

        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
