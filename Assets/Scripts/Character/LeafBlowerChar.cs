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
        }

        m_Blow = false;
        m_Interact = false;
    }

    public void SetNpcInRange(NPC npc)
    {
        npcInRange = npc;
    }

}
