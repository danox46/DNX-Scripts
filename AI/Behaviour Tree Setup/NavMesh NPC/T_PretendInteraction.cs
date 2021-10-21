using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

public class T_PretendInteraction : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public T_PretendInteraction(CoreNavMeshNPC currentChar) : base()
    {
        thisChar = currentChar;
    }

    public override NodeState Evaluate()
    {
        thisChar.PretendToInteract();
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
