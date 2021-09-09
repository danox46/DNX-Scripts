using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Initial code from Mina Pêcheux https://mina-pecheux.medium.com/
namespace DnxBehaviorTree
{

    public abstract class BaseTree : MonoBehaviour
    {
        private BaseNode m_root = null;

        protected void Start()
        {
            m_root = SetupTree();
        }

        protected virtual void Update()
        {
            if (m_root != null)
                m_root.Evaluate();
        }

        public BaseNode Root => m_root;
        protected abstract BaseNode SetupTree();
    }

}
