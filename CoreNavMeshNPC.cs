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
        [SerializeField] protected float timeToTalk; //CD time for pretending to talk
        [SerializeField] protected NavMeshAgent m_NavMeshAgent;

        // Start is called before the first frame update
        void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //BT functions
        //Determines if the char is busy with a priority task that should prevent it from starting idle functions
        public abstract bool CharIsEngaged();

        //If the NPC is engaged to someone it should be looking at them
        public abstract void LookAtTarget(Transform target);

        //If the NPC is engaged to the player the interaction will managed by the Dialog manager
        public abstract bool IsEngagedToPlayer();

        //If the NPC is not engaged to the player is assumed engaged to other NPC and should pretend to talk
        public abstract void PretendToTalk();

        //This check allows for an NPC that doesn't patrol while this returns false
        public abstract bool CharHasDestinations();

        //This prevents the char from setting a new destination if it's already on it's way to one
        public virtual bool CharIsAdvancingToDestination()
        {
            return m_NavMeshAgent.hasPath;
        }

        public abstract Vector3 UpdateDestination(List<Vector3> availableDestinations);

        public virtual void SetDestination(List<Vector3> destinations)
        {
            m_NavMeshAgent.SetDestination(UpdateDestination(destinations));
        }

    }
}
