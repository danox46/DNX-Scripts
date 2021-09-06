using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class T_PretendToTalk : BaseNode
{
    private NPC thisChar;

    public T_PretendToTalk(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        thisChar.PretendInteraction();
        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
