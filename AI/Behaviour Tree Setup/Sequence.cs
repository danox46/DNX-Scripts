using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DnxBehaviorTree
{

    public class Sequence : BaseNode
    {
        private bool m_isRandom;

        public Sequence() : base() { m_isRandom = false; }
        public Sequence(bool isRandom) : base() { m_isRandom = isRandom; }
        public Sequence(List<BaseNode> children, bool isRandom = false) : base(children)
        {
            m_isRandom = isRandom;
        }

        public static List<T> Shuffle<T>(List<T> list)
        {
            System.Random r = new System.Random();
            return list.OrderBy(x => r.Next()).ToList();
        }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            if (m_isRandom)
                m_Children = Shuffle(m_Children);

            foreach (BaseNode node in m_Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        m_state = NodeState.FAILURE;
                        return m_state;
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
            m_state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return m_state;
        }
    }
}
