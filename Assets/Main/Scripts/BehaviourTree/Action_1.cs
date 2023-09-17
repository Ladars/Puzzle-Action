using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BEH.AI;

public class Action_1 : BTActionNode
{
    public override NodeState Tick()
    {
        DoAction2();
        return NodeState.Success;
    }
    private void DoAction2()
    {
        Debug.Log("¶¯×÷2");
    }
}


