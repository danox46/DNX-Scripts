using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

//This is set to work with the NavMeshAgent. By default it works with the NavMeshAgent.hasPath get function
//Remeber to adapt CharIsAdvancingToDestination if using a different pathfinding method
public class C_hasPath : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_hasPath(CoreNavMeshNPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        //By default checks if the NavMeshAgent has a path
        m_state = thisChar.CharIsAdvancingToDestination() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
