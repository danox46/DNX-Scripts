using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

public class C_hasPath : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_hasPath(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.CharIsAdvancingToDestination() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
