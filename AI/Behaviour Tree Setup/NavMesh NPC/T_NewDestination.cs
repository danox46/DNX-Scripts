using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNX.Characters;
using DnxBehaviorTree;

//The NPC finds a new destination when idle
public class T_NewDestination : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public T_NewDestination(CoreNavMeshNPC currentChar) : base()
    {
        thisChar = currentChar;
    }

    public override NodeState Evaluate()
    {
        //Using the Set destination of the context
        thisChar.SetDestination();
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
