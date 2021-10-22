using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

//This check allows for a static NPC if no destinations were manually added
public class C_hasDestinations : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_hasDestinations(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        //Checks if char has destinations in the current context
        m_state = thisChar.CharHasDestinations() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
