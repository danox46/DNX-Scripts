using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DNX.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class CoreNavMeshNPC : MonoBehaviour
    {
        [Header("Core NPC Variables")]
        //[SerializeField] protected float timeToTalk; //CD time for pretending to talk
        protected NavMeshAgent m_NavMeshAgent;
        [SerializeField] protected List<Transform> currentDestinations = new List<Transform>();
        [SerializeField] protected Transform _engagedTo;
        public Transform EngagedTo { get => _engagedTo; }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        //BT functions
        /// <summary>
        /// <para>By default, sets the NPC to stop and assigns the engagement target</para>
        /// </summary>
        /// <param name="target">The target to be engaged by the NPC</param>
        public virtual void EngageChar(Transform target)
        {
            m_NavMeshAgent.SetDestination(transform.position);
            _engagedTo = target;
        }

        /// <summary>
        /// Determines if the char is busy with a priority task that should prevent it from starting idle functions
        /// </summary>
        /// <returns></returns>
        public virtual bool CharIsEngaged() { return _engagedTo != null; }

        /// <summary>
        /// If the NPC is engaged, it should be looking at the target
        /// </summary>
        /// <param name="target"> What the NPC is engaged to </param>
        public abstract void LookAtTarget(Transform target);

        /// <summary>
        /// If the NPC is engaged to the player the interaction will managed by the Dialog manager
        /// </summary>
        /// <returns></returns>
        public abstract bool IsEngagedToPlayer();

        /// <summary>
        /// <para>If the NPC is not engaged to the player is assumed engaged to other NPC and should pretend to talk</para>
        /// <para>If you want the NPC to perform aditional interactions spesify that here</para>
        /// <example> Switch the target and set to perform the right action
        /// <code>
        /// switch (target)
        ///    {
        ///        case "NPC":
        ///            CallTalkAnimation();
        ///            break;
        ///        case "Tree":
        ///            CallChopWoodAnimation();
        ///            break;
        ///    }
        /// </code>
        /// </example>
        /// </summary>
        public abstract void PretendToInteract();

        /// <summary>
        /// This check allows for an NPC that doesn't patrol while this returns false
        /// </summary>
        /// <returns></returns>
        public abstract bool CharHasDestinations();

        /// <summary>
        /// <para>Prevents the NPC from setting a new destination if it's already on it's way to one</para>
        /// <para>Remember to override this if you use a different pathfinding method</para>
        /// </summary>
        /// <returns></returns>
        public virtual bool CharIsAdvancingToDestination() { return m_NavMeshAgent.hasPath; }

        /// <summary>
        /// Determines which destination the NPC should visit next
        /// </summary>
        /// <param name="availableDestinations">Points the NPC should visit when idle</param>
        /// <returns></returns>
        //We take a new list of available destinations in case we want an event to spesify spesific destinations
        public abstract Vector3 UpdateDestination(List<Transform> availableDestinations);

        /// <summary>
        /// Allows to request a destination from outside the class
        /// </summary>
        public virtual void SetDestination() { m_NavMeshAgent.SetDestination(UpdateDestination(currentDestinations)); }

        public virtual Vector3 GetDestination() { return m_NavMeshAgent.destination; }



    }
}
