using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class LeafBlowerChar : FirstPersonController
{

    public Collider blowerAOE;
    private bool m_Blow;

    protected override void Update()
    {
        base.Update();

        if (!m_Blow)
        {
            m_Blow = CrossPlatformInputManager.GetButton("Fire1");
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

        m_Blow = false;
    }

}
