using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
public class T_LookAtTarget : BaseNode
{
    private NPC thisChar;

    public T_LookAtTarget(NPC currentBot) : base()
    {
        thisChar = currentBot;
    }

    public override NodeState Evaluate()
    {
        thisChar.transform.LookAt(thisChar.engagedWith);
        thisChar.transform.rotation = new Quaternion(0, thisChar.transform.rotation.y, 0, thisChar.transform.rotation.w);

        m_state = NodeState.SUCCESS;

        return m_state;
    }
}
