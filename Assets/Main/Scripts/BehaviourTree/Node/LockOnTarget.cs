using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


namespace EnemyBehavior
{
	public class LockOnTarget : Action
	{
        public Animator _animator;
        private int lockOnID = Animator.StringToHash("LockOn");
        public SharedTransform currentTarget;

        public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
            lockOnTarget();

            return TaskStatus.Running;
		}
        private void lockOnTarget()
        {
            if (_animator.CheckAnimationTag("Motion") && currentTarget != null)
            {
                _animator.SetFloat(lockOnID, 1f);
                transform.root.rotation = transform.LockOnTarget(currentTarget, transform.root.transform, 50);
            }
            else
            {
                _animator.SetFloat(lockOnID, 0);
            }
        }
    }
}

