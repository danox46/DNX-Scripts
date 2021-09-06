using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_CharHasPath : BaseNode
{
    private NPC thisChar;

    public C_CharHasPath(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.m_NavMesh.hasPath ? NodeState.SUCCESS : NodeState.FAILURE;
        


        return m_state;
    }
}
