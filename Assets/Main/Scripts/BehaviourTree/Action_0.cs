using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BEH.AI;
public class Action_0 : BTActionNode
{

    /// <summary>
    /// 相当于mono的update
    /// </summary>
    /// <returns></returns>
    public override NodeState Tick()
    {
        DoAction();
        return NodeState.Success;
    }
    private void DoAction()
    {
        Debug.Log("动作1");
    }
}
