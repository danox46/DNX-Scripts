using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class C_CharEngagedToPlayer : BaseNode
{
    private NPC thisChar;

    public C_CharEngagedToPlayer(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        m_state = thisChar.engagedWith.tag == "Player" ? NodeState.SUCCESS : NodeState.FAILURE;

        return m_state;
    }
}
