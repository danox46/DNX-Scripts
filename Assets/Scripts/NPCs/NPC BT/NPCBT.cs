using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class NPCBT : BaseTree
{
    private NPC currentChar;

    private void Awake()
    {
        currentChar = GetComponent<NPC>();
    }


    protected override BaseNode SetupTree()
    {
        BaseNode _root;

        _root = new Selector(new List<BaseNode>
        {
            new Sequence(new List<BaseNode>{ 
                new C_CharIsEngaged(currentChar),
                new T_LookAtTarget(currentChar)
            }),
            new Selector(new List<BaseNode>
            {
                new Inverter(new List<BaseNode>{new C_ChasHasDestinations(currentChar)}),
                new C_CharHasPath(currentChar),
                new Timer(5f, new List<BaseNode>{ new T_SetDestination(currentChar)})
            })

        });

             return _root;
    }
}
