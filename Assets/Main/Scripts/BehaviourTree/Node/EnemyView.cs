using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;


namespace EnemyBehavior
{
    public class EnemyView : Action
    {
        [SerializeField] private Transform detectionCenter;
        [SerializeField] Collider[] collidersTarget = new Collider[1];
        public SharedTransform currentTarget;
        [SerializeField] private float detectionRange;
        [SerializeField] private LayerMask EnemyMask;
        [SerializeField] private LayerMask ObstacleMask;
        public SharedFloat DistanceToPlayer;
        public override TaskStatus OnUpdate()
        {
            AIView();
            GetCurrentTargetDistance();
            return TaskStatus.Running;
        }
        private void AIView()
        {
            int targetCount = Physics.OverlapSphereNonAlloc(detectionCenter.position, detectionRange, collidersTarget, EnemyMask);
            if (targetCount > 0)
            {
                Vector3 direcction = (collidersTarget[0].transform.position - transform.root.position).normalized;
                if (!Physics.Raycast(transform.root.position + transform.root.up * .5f, direcction, out var hit, detectionRange, ObstacleMask))
                {
                    if (Vector3.Dot(direcction, transform.root.forward) > 0.25f)
                    {
                        currentTarget.Value = collidersTarget[0].transform;                       
                    }
                }
            }
            else
            {
                currentTarget = null;
            }
        }
        public float GetCurrentTargetDistance()
        {
            if (currentTarget != null)
            {
                //  Debug.Log(Vector3.Distance(currentTarget.Value.position, transform.root.position));
                DistanceToPlayer.Value = Vector3.Distance(currentTarget.Value.position, transform.root.position);

                return DistanceToPlayer.Value;
            }
            else
            {
                DistanceToPlayer = 9999999;
                return DistanceToPlayer.Value;
            }
        }
    }
}


