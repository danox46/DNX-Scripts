using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_CharIsEngaged : BaseNode
{
    private NPC thisChar;

    public C_CharIsEngaged(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.Engaged ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }


}
