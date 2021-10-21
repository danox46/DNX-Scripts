using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;
using DNX.Characters;

public class CoreNavMeshNPCBT : BaseTree
{
    private CoreNavMeshNPC currentChar;
    [SerializeField] private float interactionTime = 4f;
    [SerializeField] private float destinationTime = 6f;

    private void Awake()
    {
        currentChar = GetComponent<CoreNavMeshNPC>();
    }


    protected override BaseNode SetupTree()
    {
        BaseNode _root;

        _root = new Selector(new List<BaseNode>
        {
            new Sequence(new List<BaseNode>{
                new C_isEngaged(currentChar),
                new T_LookAtEngaged(currentChar),
                new Selector(new List<BaseNode>
                {
                    new C_isEngagedToPlayer(currentChar),
                    new Timer(interactionTime, new List<BaseNode> { new T_PretendInteraction(currentChar)})
                })
            }),
            new Selector(new List<BaseNode>
            {
                new Inverter(new List<BaseNode>{new C_hasDestinations(currentChar)}),
                new C_hasPath(currentChar),
                new Timer(destinationTime, new List<BaseNode>{ new T_NewDestination(currentChar)})
            })

        });

        return _root;
    }
}
