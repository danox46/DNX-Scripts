using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DnxBehaviorTree
{

    public class Inverter : BaseNode
    {
        public Inverter() : base() { }
        public Inverter(List<BaseNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            if (!HasChildren) return NodeState.FAILURE;
            switch (m_Children[0].Evaluate())
            {
                case NodeState.FAILURE:
                    m_state = NodeState.SUCCESS;
                    return m_state;
                case NodeState.SUCCESS:
                    m_state = NodeState.FAILURE;
                    return m_state;
                case NodeState.RUNNING:
                    m_state = NodeState.RUNNING;
                    return m_state;
                default:
                    m_state = NodeState.FAILURE;
                    return m_state;
            }
        }
    }
}
