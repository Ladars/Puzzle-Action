using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BEH.AI;

public abstract class BehaviourTree : MonoBehaviour
{
    protected BTRootNode m_Root;

    protected virtual void Start()
    {
        m_Root = new BTRootNode();
    }

    /// <summary>
    /// 添加子节点函数
    /// </summary>
    /// <param name="child"></param>
    protected void AddNode(params BTNode[] child)
    {
        m_Root.Open(child);
    }
    protected virtual void Update()
    {
        m_Root.Tick();
    }

}
