using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    public GameObject interactionClue;
        
    //Stats
    [SerializeField] private float speed;

    //AI
    [SerializeField] List<Transform> patrolPoints;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimToWalk();

    }

    public void Move(Vector3 direction)
    {
        Vector3 movement = direction.normalized;
        Vector3 newVelocity = new Vector3(movement.x * speed, m_Rigidbody.velocity.y, movement.z);
        transform.LookAt(newVelocity + transform.position);
        m_Rigidbody.velocity = newVelocity;

    }

    private void SetAnimToWalk()
    {
        if (m_Rigidbody.velocity.magnitude > 0.01)
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
        if(other.tag == "Player")
        {
            if(!interactionClue.activeSelf)
                interactionClue.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (interactionClue.activeSelf)
                interactionClue.SetActive(false);
        }
    }

}
