using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

public class C_isEngaged : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_isEngaged(CoreNavMeshNPC currentChar) : base()
    {
        thisChar = currentChar;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.CharIsEngaged() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
