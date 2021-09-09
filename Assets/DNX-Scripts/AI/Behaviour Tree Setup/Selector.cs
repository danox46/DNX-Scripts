using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Initial code from Mina Pêcheux https://mina-pecheux.medium.com/
namespace DnxBehaviorTree {

    public class Selector : BaseNode
    {
        public Selector() : base() { }
        public Selector(List<BaseNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (BaseNode node in m_Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        m_state = NodeState.SUCCESS;
                        return m_state;
                    case NodeState.RUNNING:
                        m_state = NodeState.RUNNING;
                        return m_state;
                    default:
                        continue;
                }
            }
            m_state = NodeState.FAILURE;
            return m_state;
        }
    }
}


