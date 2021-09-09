using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DnxBehaviorTree
{

    public class Timer : BaseNode
    {
        private float m_delay;
        private float m_time;

        public delegate void TickEnded();
        public event TickEnded onTickEnded;

        public Timer(float delay, TickEnded onTickEnded = null) : base()
        {
            m_delay = delay;
            m_time = m_delay;
            this.onTickEnded = onTickEnded;
        }
        public Timer(float delay, List<BaseNode> children, TickEnded onTickEnded = null)
            : base(children)
        {
            m_delay = delay;
            m_time = m_delay;
            this.onTickEnded = onTickEnded;
        }

        public override NodeState Evaluate()
        {
            if (!HasChildren) return NodeState.FAILURE;
            if (m_time <= 0)
            {
                m_time = m_delay;
                m_state = m_Children[0].Evaluate();
                if (onTickEnded != null)
                    onTickEnded();
                m_state = NodeState.SUCCESS;
            }
            else
            {
                m_time -= Time.deltaTime;
                m_state = NodeState.RUNNING;
            }
            return m_state;
        }
    }
}
