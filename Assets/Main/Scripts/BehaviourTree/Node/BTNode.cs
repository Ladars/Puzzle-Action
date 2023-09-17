using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BEH.AI 
{
    public enum NodeState
    {
        Failure,
        Success,      
        Running
    }
    public abstract class BTNode 
    {
        public abstract NodeState Tick();
    }
    /// <summary>
    /// �ֲ�ڵ�
    /// </summary>
    public abstract class BTbifurcation :BTNode
    {
        protected List<BTNode> childrenNode = new List<BTNode>();
        protected int currentChildIndex=0;

        public virtual BTbifurcation Open(params BTNode[] _childrenNode)
        {
            for (int i = 0; i < _childrenNode.Length; i++)
            {
                childrenNode.Add(_childrenNode[i]);
            }
            return this;
        }
    }
    /// <summary>
    /// ˳��ڵ㣬����ִ�����нڵ㣬ֻҪ��һ���ڵ�ִ��ʧ�ܣ��Ͳ�ִ�к����ڵ�
    /// </summary>
    public class BTSequenceNode : BTbifurcation
    {
        public override NodeState Tick()
        {
            var childNode = childrenNode[currentChildIndex].Tick();
            switch (childNode)
            {
                case NodeState.Success:
                    currentChildIndex++;
                    if (currentChildIndex ==childrenNode.Count)
                    {
                        currentChildIndex = 0;
                    }
                    return NodeState.Success;
                   

                case NodeState.Failure:
                    currentChildIndex = 0;
                    return NodeState.Failure;
               

                case NodeState.Running:
                    return NodeState.Running;
                                  
            }
            return NodeState.Success;
        }
    }
    /// <summary>
    /// ѡ��ڵ㣬����ִ�����нڵ㣬ֻҪ��һ���ڵ�ִ�гɹ����Ͳ�ִ�к����ڵ�
    /// </summary>
    public class BTSelectNode : BTbifurcation
    {
        public override NodeState Tick()
        {
            var childNode = childrenNode[currentChildIndex].Tick();
            switch (childNode)
            {
                case NodeState.Success:
                    currentChildIndex = 0;                   
                    return NodeState.Success;


                case NodeState.Failure:
                    currentChildIndex++;
                    if (currentChildIndex == childrenNode.Count)
                    {
                        currentChildIndex = 0;
                    }
                    return NodeState.Failure;


                case NodeState.Running:
                    return NodeState.Running;

            }
            return NodeState.Success;
        }
    }
    public class BTRootNode : BTbifurcation
    {
        public bool isStop=false;
        public override NodeState Tick()
        {
            var childNode = childrenNode[currentChildIndex].Tick();
            while (true)
            {
                switch (childNode)
                {
                   
                    case NodeState.Running:
                        return NodeState.Running;
                    default:
                        currentChildIndex++;
                        if (currentChildIndex ==childrenNode.Count)
                        {
                            currentChildIndex = 0;
                            return NodeState.Success;
                        }
                        continue;
                      
                }
            }
        }
    }

    public abstract class BTActionNode : BTNode
    {

    }
    public abstract class BTConditionNode : BTNode
    {

    }
  

}


