using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    public GameObject interactionClue;
    public NavMeshAgent m_NavMesh;

    //Dialog
    public List<Dialog> availableDialogs;
    private bool engaged;
    public Transform engagedWith;
    public string greeting;

    public bool Engaged { get => engaged; }

    public List<Dialog> Dialogs { get => availableDialogs; }

    //Stats
    [SerializeField] private string npcName;

    public string GivenName { get => npcName; }

    [SerializeField] private float speed;

    //AI
    public Transform currentPatrolPoint;
    [SerializeField] List<Transform> patrolPoints;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        engaged = false;
        m_NavMesh = GetComponent<NavMeshAgent>();

        if (patrolPoints.Count > 0)
            currentPatrolPoint = patrolPoints[0];
        else
            currentPatrolPoint = null;
    }

    private void Update()
    {
        //Updating animator when the has velocity
        SetAnimToWalk();

    }

    /*public void Move(Vector3 direction)
    {
        Vector3 movement = direction.normalized;
        Vector3 newVelocity = new Vector3(movement.x * speed, m_Rigidbody.velocity.y, movement.z);

        //transform.LookAt was giving me trouble with the X rotation. Need to check this
        transform.LookAt(newVelocity + transform.position);
        m_Rigidbody.velocity = newVelocity;

    }*/

    private void SetAnimToWalk()
    {
        if (m_Rigidbody.velocity.magnitude > 0.6)
        {
            if (!m_Animator.GetBool("Walking"))
                m_Animator.SetBool("Walking", true);
        }
        else
        {
            if (m_Animator.GetBool("Walking"))
                m_Animator.SetBool("Walking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player enters interaction range allow for interaction
        if(other.tag == "Player")
        {
            if (!interactionClue.activeSelf)
            {
                other.GetComponent<LeafBlowerChar>().SetNpcInRange(this);
                interactionClue.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Forbiding interaction if player get's out of range
        if (other.tag == "Player")
        {
            if (interactionClue.activeSelf)
            {
                other.GetComponent<LeafBlowerChar>().SetNpcInRange(null);
                interactionClue.SetActive(false);
            }
        }
    }

    //I did this function to add anything we might need on the dialog trigger for polishing
    public void SetNPCEngaged(Transform engagedTo)
    {
        engagedWith = engagedTo;
        engaged = true;
    }

    public void DisengageNPC()
    {
        engaged = false;
        engagedWith = null;
    }

    public void EndWait()
    {
        //Change patrol point
        if(patrolPoints.Count > 0)
            currentPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Count - 1)];
    }

}
