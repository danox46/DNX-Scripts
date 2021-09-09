using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class T_wakeUp : BaseNode
{

    private Sweepbot thisChar;

    public T_wakeUp(Sweepbot currentbot) : base ()
    {
        thisChar = currentbot;
    }

    public override NodeState Evaluate()
    {
        thisChar.WakeUp();
        m_state = NodeState.SUCCESS;

        return m_state;
    }

}
