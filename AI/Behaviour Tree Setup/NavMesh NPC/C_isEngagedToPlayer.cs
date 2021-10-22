using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNX.Characters;
using DnxBehaviorTree;


//Check if the NPC is engaged to the player, so it can ignore other events
public class C_isEngagedToPlayer : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_isEngagedToPlayer(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        //Checks if the NPC is engaged to a player
        m_state = thisChar.IsEngagedToPlayer() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
