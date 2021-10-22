using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;


//The CoreNavMeshNPC Behaiviour Tree handles the actions of an NPC that can, when spesified, patrol ramdomly trough a list of destinations
//It can engage a target when promted, which will make it stop until the engagement is broken with "CoreNavMeshNPC.EngageNPC(null)"
public class CoreNavMeshNPCBT : BaseTree
{
    private CoreNavMeshNPC currentChar;

    [SerializeField] private float interactionTime = 4f; //Allows to serialize the time between pretend intearations from the inspector
    [SerializeField] private float destinationTime = 6f; //Allows to serialize the time an NPC will spend idle in a particular place before setting a new random destination

    private void Awake()
    {
        currentChar = GetComponent<CoreNavMeshNPC>();
    }


    protected override BaseNode SetupTree()
    {
        BaseNode _root;

        //The BT setup starts with a selector, which will devide tasks from idle actions
        _root = new Selector(new List<BaseNode>
        {
            new Sequence(new List<BaseNode>{ //---Tasks---
                new C_isEngaged(currentChar), //If the NPC isn't "engaged" the sequence will return a failure, which will lead the tree to idle acctions
                new T_LookAtEngaged(currentChar), //If it is engaged, then it will look at it's target and run the interaction
                new Selector(new List<BaseNode>
                {
                    new C_isEngagedToPlayer(currentChar), //If the NPC is engaged to a player, the interaction will be handled by a dialog system
                    new Timer(interactionTime, new List<BaseNode> { new T_PretendInteraction(currentChar)}) //If the NPC is not engaged to a player it will pretend to interact as defined
                })
            }),
            new Selector(new List<BaseNode> //---Idle actions---
            {
                new Inverter(new List<BaseNode>{new C_hasDestinations(currentChar)}), //If the NPC is not engaged, check if it should be looking for a place to go
                new C_hasPath(currentChar), //Then, it checks if it's already going to a place
                new Timer(destinationTime, new List<BaseNode>{ new T_NewDestination(currentChar)}) //If it's not engaged, and not traveling to a destination, it waits until it's time to look for a new one
            })

        });

        return _root;
    }
}
