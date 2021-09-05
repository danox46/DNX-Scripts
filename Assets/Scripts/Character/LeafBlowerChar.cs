using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class LeafBlowerChar : FirstPersonController
{
    public DialogManager dialogManager;
    public Collider blowerAOE;
    private bool m_Blow;
    private bool m_Interact;
    private NPC npcInRange;
    private QuestItem itemInRage;
    public QuestSystem m_QuestSystem;

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
        npcInRange = null;
        itemInRage = null;
    }

    protected override void Update()
    {
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

        if (m_Blow)
        {
            blowerAOE.enabled = true;
        }
        else
        {
            blowerAOE.enabled = false;
        }

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

    public void SetNpcInRange(NPC npc)
    {
        npcInRange = npc;
    }

    public void SetItemInRange(QuestItem questItem)
    {
        itemInRage = questItem;
    }

    protected override void RotateView()
    {
        base.RotateView();
        m_MouseLook.BlowerLookRotationY(blowerAOE.transform);
    }

    public void EngageChar()
    {
        m_MouseLook.LockCursor(false);
    }

    public void DisengageChar()
    {
        m_MouseLook.LockCursor(true);
    }

}
