using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class NodeTest : Action
{
    public override TaskStatus OnUpdate()
    {
        Debug.Log("behavious test");
        return TaskStatus.Running;
    }
}
