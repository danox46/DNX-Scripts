using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;


//Checks if the char is busy with something that should only be interrupted by player interaction
public class C_isEngaged : BaseNode
{
    private CoreNavMeshNPC thisChar;

    public C_isEngaged(CoreNavMeshNPC currentChar) : base()
    {
        thisChar = currentChar;
    }

    public override NodeState Evaluate()
    {
        //Check if the char is busy
        m_state = thisChar.CharIsEngaged() ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
