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

        float speed;
        GetInput(out speed);
        // always move along the camera forward as it is the direction that it being aimed at
        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

        // get a normal for the surface that is being touched to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;

        if (m_MouseLook.CursorIsLocked)
        {

            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

            }

            if (m_Jump)
            {
                RaycastHit verticalHitInfo;

                Physics.Raycast(new Ray(transform.position, Vector3.down), out verticalHitInfo, 10f);

                if (verticalHitInfo.collider != null) 
                {
                    m_MoveDir.y = m_JumpSpeed / Mathf.Sqrt(verticalHitInfo.distance/2);
                    m_Jump = false;
                    m_Jumping = true;
                }

                m_MoveDir.x = m_MoveDir.x / 4;
                m_MoveDir.z = m_MoveDir.z / 4;


                PlayJumpSound();
            }

            if(!m_CharacterController.isGrounded)
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);




            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
        }

        m_MouseLook.UpdateCursorLock();

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
        transform.LookAt(new Vector3(npcInRange.transform.position.x, npcInRange.transform.position.y + 0.3f, npcInRange.transform.position.z));
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

    public void BlowerJetPack()
    {
       
    }

}
