using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

//The BT sets this to be called on every update that the char is engaged to manage a smooth look at without extra updates
public class T_LookAtEngaged : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public T_LookAtEngaged(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        //Use the context look at target
        thisChar.LookAtTarget(thisChar.EngagedTo);

        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
