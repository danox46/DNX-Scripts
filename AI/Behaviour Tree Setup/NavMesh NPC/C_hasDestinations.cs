using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

public class C_hasDestinations : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_hasDestinations(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.CharHasDestinations() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
