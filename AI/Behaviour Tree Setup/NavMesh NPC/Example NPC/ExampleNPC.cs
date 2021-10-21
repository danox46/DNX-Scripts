using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNX.Characters;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ExampleNPC : CoreNavMeshNPC
{
    public GameObject interactionClue;
    private Animator m_Animator;
    [SerializeField] private List<Sprite> interactions;
    public float talkRate;

    protected override void Start()
    {
        base.Start();

        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_NavMeshAgent.hasPath)
            m_Animator.SetBool("Walking", true);
        else
            m_Animator.SetBool("Walking", false);
    }

    public override bool CharHasDestinations() { return currentDestinations.Count > 0; }

    public override bool IsEngagedToPlayer() { return _engagedTo.tag == "Player"; }


    public override void LookAtTarget(Transform target)
    {
        transform.LookAt(_engagedTo);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    public override void PretendToInteract()
    {
        int interactionRoll = Random.Range(0, interactions.Count);
        interactionClue.GetComponent<Image>().enabled = true;
        interactionClue.GetComponent<Image>().sprite = interactions[interactionRoll];

        m_Animator.SetTrigger("Talk");
    }

    public override Vector3 UpdateDestination(List<Transform> availableDestinations)
    {
        if (availableDestinations.Count > 0)
            return availableDestinations[Random.Range(0, availableDestinations.Count)].position;
        else
            return transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Npc" && !CharIsEngaged() && !other.GetComponent<CoreNavMeshNPC>().CharIsEngaged())
        {
            other.GetComponent<CoreNavMeshNPC>().EngageChar(transform);
            EngageChar(other.transform);
        }
    }

    public void DisengageNPC() { _engagedTo = null; }

}
