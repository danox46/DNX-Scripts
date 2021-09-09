using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DnxBehaviorTree;

public class BT_Sweepbot : BaseTree
{
    Sweepbot currentBot;

    private void Awake()
    {
        currentBot = GetComponent<Sweepbot>();
    }


    protected override BaseNode SetupTree()
    {
        BaseNode _root;

        _root = new Selector(new List<BaseNode> 
        {
            new Inverter(new List<BaseNode>{new C_CharIsAlive(currentBot) }),
            new Sequence( new List<BaseNode>
            {
                new C_CharSleeping(currentBot),
                new Selector(new List<BaseNode>
                    {
                        new Sequence(new List<BaseNode>
                        {
                        new C_TargetInDetectRange(currentBot),
                        new T_wakeUp(currentBot)
                        }),
                        new Selector(new List<BaseNode>
                        {
                            new Sequence(new List<BaseNode>
                            {
                                new C_isPatrolling(currentBot),
                                new Selector(new List<BaseNode>
                                {
                                    new Sequence(new List<BaseNode>
                                    {
                                        new C_CharInPatrolPoint(currentBot),
                                        new Timer(5, new List<BaseNode>(){ new T_EndWait(currentBot)},
                                            delegate
                                            {
                                                Debug.Log("Changing Patrol Point");
                                            })
                                    }),
                                    new T_MoveToPatrol(currentBot)
                                })
                            }),
                            new T_Sleep()
                        })
                    })
            }),
            
            new Sequence(new List<BaseNode>
            {
                new C_CharIsAble(currentBot),
                new Selector(new List<BaseNode>
                {
                    new Sequence(new List<BaseNode>
                    {
                        new C_TargetInAttackRange(currentBot),
                        new T_Attack(currentBot)
                    }),
                    new T_MoveToTarget(currentBot)
                })
            }),
        });
           

        return _root;
    }
}
