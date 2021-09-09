using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class T_Sleep : BaseNode
{
    public override NodeState Evaluate()
    {
        return NodeState.SUCCESS;
    }
}
