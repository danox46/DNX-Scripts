using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DnxBehaviorTree
{

    public class Parallel : BaseNode
    {
        public Parallel() : base() { }
        public Parallel(List<BaseNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            int nFailedChildren = 0;
            foreach (BaseNode node in m_Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        nFailedChildren++;
                        continue;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        m_state = NodeState.SUCCESS;
                        return m_state;
                }
            }
            if (nFailedChildren == m_Children.Count)
                m_state = NodeState.FAILURE;
            else
                m_state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return m_state;
        }
    }
}