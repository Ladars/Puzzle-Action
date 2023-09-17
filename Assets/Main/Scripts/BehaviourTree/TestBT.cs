using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BEH.AI;
public class TestBT : BehaviourTree
{
    public bool b;
    protected override void Start()
    {
        base.Start();
        AddNode(new BTSelectNode().Open(new BTSequenceNode().Open
            (
             new Condition_0(TestBool),new Action_0()),new BTSequenceNode().Open(new Action_1()) 
            ));
    }
    public bool TestBool()
    {
        return b;
    }
}
