using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BEH.AI;
using System;

public class Condition_0 : BTConditionNode
{
    private bool isTrue;
    Func<bool> m_func;

    public Condition_0(Func<bool> func)
    {
        this.m_func = func;
    }

    /// <summary>
    /// 表示该节点的行为
    /// </summary>
    /// <returns></returns>
    public override NodeState Tick()
    {
        if (m_func!=null)
        {
            return (m_func.Invoke())? NodeState.Success:NodeState.Failure;
        }
        return NodeState.Failure;
    }
}
