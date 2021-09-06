using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class T_EndWait : BaseNode
{
    private NPC thisChar;

    public T_EndWait(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        thisChar.EndWait();

        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
