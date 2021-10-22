using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

//When the NPC is set as engaged, but is not engaged to the char, it pretends to do any defined action
public class T_PretendInteraction : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public T_PretendInteraction(CoreNavMeshNPC currentChar) : base()
    {
        thisChar = currentChar;
    }

    public override NodeState Evaluate()
    {
        //Using the prented interaction from context
        thisChar.PretendToInteract();
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
