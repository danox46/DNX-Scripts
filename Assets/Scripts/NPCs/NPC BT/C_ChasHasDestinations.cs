using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_ChasHasDestinations : BaseNode
{

    private NPC thisChar;

    public C_ChasHasDestinations(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.currentPatrolPoint != null ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }

}
