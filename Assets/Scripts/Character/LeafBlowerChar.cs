using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class LeafBlowerChar : FirstPersonController
{
    //Dialog
    public DialogManager dialogManager;
    public QuestSystem m_QuestSystem;

    //Blower
    public Collider blowerAOE;
    [SerializeField] private List<GameObject> leafblowers;

    //Input
    private bool m_Blow;
    private bool m_Interact;
    private NPC npcInRange;
    private QuestItem itemInRage;

    //Stats
    public int coins;

    //Overriding start to change the m_mouselook int to include the blower
    protected override void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        m_OriginalCameraPosition = m_Camera.transform.localPosition;
        m_FovKick.Setup(m_Camera);
        m_HeadBob.Setup(m_Camera, m_StepInterval);
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_Jumping = false;
        m_AudioSource = GetComponent<AudioSource>();
        m_MouseLook.Init(transform, m_Camera.transform, blowerAOE.transform);
        m_QuestSystem = GetComponent<QuestSystem>();

        //InRange values are added by the NPC or the item on enter and exit trigger
        npcInRange = null;
        itemInRage = null;
    }

    protected override void Update()
    {
        //Getting new inputs
        base.Update();

        if (!m_Blow)
        {
            m_Blow = CrossPlatformInputManager.GetButton("Fire1");
        }

        if (!m_Interact)
        {
            m_Interact = CrossPlatformInputManager.GetButtonUp("Interact");
        }
    }

    protected override void FixedUpdate()
    {

        base.FixedUpdate();

        //Applying new inputs
        if (m_Blow)
        {
            blowerAOE.enabled = true;
        }
        else
        {
            blowerAOE.enabled = false;
        }

        //Implementing multiple option for interact
        if (m_Interact)
        {
            if(npcInRange != null)
            {
                dialogManager.LaunchDialogSequence(npcInRange);
            }

            if(itemInRage != null)
            {

                if (m_QuestSystem.CheckQuests(itemInRage))
                {
                    Debug.Log("Somone needs this, I should take it to them");
                }

                m_QuestSystem.LootItem(itemInRage);

                itemInRage = null;
            }
        }

        m_Blow = false;
        m_Interact = false;
    }

    //InRange values are added by the NPC or the item on enter and exit trigger
    public void SetNpcInRange(NPC npc)
    {
        npcInRange = npc;
    }

    public void SetItemInRange(QuestItem questItem)
    {
        itemInRage = questItem;
    }

    //Adding rotation to the blower
    protected override void RotateView()
    {
        base.RotateView();
        m_MouseLook.BlowerLookRotationY(blowerAOE.transform);
    }

    //Ignore controls when the char is busy
    public void EngageChar()
    {
        m_MouseLook.LockCursor(false);
    }

    public void DisengageChar()
    {
        m_MouseLook.LockCursor(true);
    }

    //New blowers need to be saved as prefabs and added to the char
    public void ChangeBlower(int inx)
    {
        Transform auxBloweTransform = blowerAOE.transform;
        
        GameObject newBlower = Instantiate(leafblowers[inx], this.transform);

        newBlower.transform.localPosition = auxBloweTransform.localPosition;
        newBlower.transform.localRotation = auxBloweTransform.localRotation;

        blowerAOE = newBlower.GetComponent<Collider>();

        GameObject.Destroy(auxBloweTransform.gameObject);
    }

}
