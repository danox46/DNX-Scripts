using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

public class T_LookAtEngaged : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public T_LookAtEngaged(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        //Need to make this so it's a "smooth" lookat
        thisChar.LookAtTarget(thisChar.EngagedTo);

        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
