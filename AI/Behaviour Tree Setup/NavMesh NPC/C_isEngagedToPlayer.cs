using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNX.Characters;
using DnxBehaviorTree;

public class C_isEngagedToPlayer : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_isEngagedToPlayer(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.IsEngagedToPlayer() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
