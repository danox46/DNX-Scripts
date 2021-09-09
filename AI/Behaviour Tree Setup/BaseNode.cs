using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Initial code from Mina Pêcheux https://mina-pecheux.medium.com/
namespace DnxBehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class BaseNode
    {
        protected NodeState m_state;
        public NodeState State { get => m_state; }

        private BaseNode m_parent;
        protected List<BaseNode> m_Children = new List<BaseNode>();
        private Dictionary<string, object> m_dataContext = new Dictionary<string, object>();

        public BaseNode() { m_parent = null; }
        public BaseNode(List<BaseNode> children) : this()
        {
            SetChildren(children);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetChildren(List<BaseNode> children)
        {
            foreach (BaseNode current in children)
                Attach(current);
        }

        public void Attach(BaseNode child)
        {
            m_Children.Add(child);
            child.m_parent = this;
        }

        public void Detach(BaseNode child)
        {
            m_Children.Remove(child);
            child.m_parent = null;
        }

        public object GetData(string key)
        {
            object value = null;
            if (m_dataContext.TryGetValue(key, out value))
                return value;

            BaseNode node = m_parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.m_parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (m_dataContext.ContainsKey(key))
            {
                m_dataContext.Remove(key);
                return true;
            }

            BaseNode node = m_parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.m_parent;
            }
            return false;
        }

        public void SetData(string key, object value)
        {
            m_dataContext[key] = value;
        }

        public BaseNode Parent { get => m_parent; }
        public List<BaseNode> Children { get => m_Children; }
        public bool HasChildren { get => m_Children.Count > 0; }
    }


}
