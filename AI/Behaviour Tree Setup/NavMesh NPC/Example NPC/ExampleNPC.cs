using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNX.Characters;
using UnityEngine.UI;

//This is a basic example for implementing the CoreNavMeshNPC class
//The example will let the NPC engage other NPCs and interact with them by talking 
[RequireComponent(typeof(Animator))]
public class ExampleNPC : CoreNavMeshNPC
{
    [Header("Example NPC")]
    //Pretend intearcion variables
    public GameObject interactionClue;
    private Animator m_Animator;
    [SerializeField] private List<Sprite> interactions;
    public float talkRate;

    protected override void Start()
    {
        base.Start();

        //Using the animator, we can sincronize the interactions with the core BT 
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Setting the animator to walk
        if (CharIsAdvancingToDestination())
            m_Animator.SetBool("Walking", true);
        else
            m_Animator.SetBool("Walking", false);
    }

    //The example does a basic check for destinations
    //This can be implemented as something more complext integrating it with daynight cycle or and NPC necessities system
    public override bool CharHasDestinations() { return currentDestinations.Count > 0; }

    //Using Unity "GameObejct.tag" to determine what's a player 
    public override bool IsEngagedToPlayer() { return _engagedTo.tag == "Player"; }

    //Look at is called on every update that the NPC is engaged, so it's easier to implement an efficient smooth lookout
    public override void LookAtTarget(Transform target)
    {
        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

    }

    //The example allows for only one kind of pretend interaction, in this case talking to other NPCs
    public override void PretendToInteract()
    {
        int interactionRoll = Random.Range(0, interactions.Count);
        interactionClue.GetComponent<Image>().enabled = true;
        interactionClue.GetComponent<Image>().sprite = interactions[interactionRoll];

        //The disengagement function is called from the animator to ensure the NPC won't disengage in the middle of a conversation
        m_Animator.SetTrigger("Talk");
    }

    //The example choses a new destination randomly when idle
    //This can be integrated with external system to allow for richer desition making
    //A good example is integrating it with a day/night cycle to have the NPC go home at night
    public override Vector3 UpdateDestination(List<Transform> availableDestinations)
    {
        if (availableDestinations.Count > 0)
            return availableDestinations[Random.Range(0, availableDestinations.Count)].position;
        else
            return transform.position;
    }

    //The example engages the char when another NPC enters it's trigger
    private void OnTriggerEnter(Collider other)
    {
        //Before commiting to the engagement, the NPC checks if either of them are engaged
        if(other.tag == "Npc" && !CharIsEngaged() && !other.GetComponent<CoreNavMeshNPC>().CharIsEngaged())
        {
            other.GetComponent<CoreNavMeshNPC>().EngageChar(transform);
            EngageChar(other.transform);
        }
    }

    //The example spesificies the breaking of the engagement with the basic needs of the core system
    //There are other reasons to spesify this, for example, running end of engagement animations
    public void DisengageNPC() { _engagedTo = null; }

}
